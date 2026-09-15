using System.Globalization;

namespace Octokit.Generators;

internal sealed class SvgPathParser
{
	private readonly string _data;
	private readonly List<SvgFigure> _figures = new();
	private int _index;
	private char _command;
	private SvgFigure? _figure;
	private SvgPoint _current;
	private SvgPoint _subpathStart;
	private SvgPoint? _lastCubicControl;
	private SvgPoint? _lastQuadraticControl;
	private char _previousCommand;

	public SvgPathParser(string data)
	{
		_data = data ?? throw new ArgumentNullException(nameof(data));
	}

	public IReadOnlyList<SvgFigure> Parse()
	{
		while (true)
		{
			SkipSeparators();
			if (IsAtEnd)
				break;

			if (IsCommand(CurrentCharacter))
			{
				_command = CurrentCharacter;
				_index++;
			}
			else if (_command == '\0')
			{
				Throw("an SVG command was expected");
			}

			switch (_command)
			{
				case 'M':
				case 'm':
					ParseMove(_command);
					break;
				case 'L':
				case 'l':
					ParseLine(_command);
					break;
				case 'H':
				case 'h':
					ParseHorizontalLine(_command);
					break;
				case 'V':
				case 'v':
					ParseVerticalLine(_command);
					break;
				case 'C':
				case 'c':
					ParseCubicBezier(_command);
					break;
				case 'S':
				case 's':
					ParseSmoothCubicBezier(_command);
					break;
				case 'Q':
				case 'q':
					ParseQuadraticBezier(_command);
					break;
				case 'T':
				case 't':
					ParseSmoothQuadraticBezier(_command);
					break;
				case 'A':
				case 'a':
					ParseArc(_command);
					break;
				case 'Z':
				case 'z':
					CloseFigure();
					_command = '\0';
					break;
				default:
					Throw($"unsupported SVG command '{_command}'");
					break;
			}
		}

		CompleteFigure();
		return _figures;
	}

	private bool IsAtEnd => _index >= _data.Length;

	private char CurrentCharacter => _data[_index];

	private void ParseMove(char command)
	{
		var isRelative = char.IsLower(command);
		var isFirstPoint = true;

		while (CanReadNumber())
		{
			var point = ReadPoint(isRelative);
			var isMovePoint = isFirstPoint;
			if (isFirstPoint)
			{
				CompleteFigure();
				_figure = new SvgFigure(point);
				_subpathStart = point;
				isFirstPoint = false;
			}
			else
			{
				AddSegment(new SvgLineSegment(point));
			}

			_current = point;
			_previousCommand = isMovePoint ? command : isRelative ? 'l' : 'L';
			ClearControlPoints();
		}

		if (isFirstPoint)
			Throw("the move command has no coordinates");

		_command = isRelative ? 'l' : 'L';
	}

	private void ParseLine(char command)
	{
		var isRelative = char.IsLower(command);
		var hasPoint = false;

		while (CanReadNumber())
		{
			var point = ReadPoint(isRelative);
			AddSegment(new SvgLineSegment(point));
			_current = point;
			_previousCommand = command;
			ClearControlPoints();
			hasPoint = true;
		}

		if (!hasPoint)
			Throw("the line command has no coordinates");
	}

	private void ParseHorizontalLine(char command)
	{
		var isRelative = char.IsLower(command);
		var hasCoordinate = false;

		while (CanReadNumber())
		{
			var x = ReadNumber();
			var point = new SvgPoint(isRelative ? _current.X + x : x, _current.Y);
			AddSegment(new SvgLineSegment(point));
			_current = point;
			_previousCommand = command;
			ClearControlPoints();
			hasCoordinate = true;
		}

		if (!hasCoordinate)
			Throw("the horizontal line command has no coordinates");
	}

	private void ParseVerticalLine(char command)
	{
		var isRelative = char.IsLower(command);
		var hasCoordinate = false;

		while (CanReadNumber())
		{
			var y = ReadNumber();
			var point = new SvgPoint(_current.X, isRelative ? _current.Y + y : y);
			AddSegment(new SvgLineSegment(point));
			_current = point;
			_previousCommand = command;
			ClearControlPoints();
			hasCoordinate = true;
		}

		if (!hasCoordinate)
			Throw("the vertical line command has no coordinates");
	}

	private void ParseCubicBezier(char command)
	{
		var isRelative = char.IsLower(command);
		var hasSegment = false;

		while (CanReadNumber())
		{
			var point1 = ReadPoint(isRelative);
			var point2 = ReadPoint(isRelative);
			var point3 = ReadPoint(isRelative);
			AddSegment(new SvgBezierSegment(point1, point2, point3));
			_current = point3;
			_lastCubicControl = point2;
			_lastQuadraticControl = null;
			_previousCommand = command;
			hasSegment = true;
		}

		if (!hasSegment)
			Throw("the cubic Bézier command has no coordinates");
	}

	private void ParseSmoothCubicBezier(char command)
	{
		var isRelative = char.IsLower(command);
		var hasSegment = false;

		while (CanReadNumber())
		{
			var point1 = IsCubicCommand(_previousCommand) && _lastCubicControl.HasValue
				? Reflect(_lastCubicControl.Value, _current)
				: _current;
			var point2 = ReadPoint(isRelative);
			var point3 = ReadPoint(isRelative);
			AddSegment(new SvgBezierSegment(point1, point2, point3));
			_current = point3;
			_lastCubicControl = point2;
			_lastQuadraticControl = null;
			_previousCommand = command;
			hasSegment = true;
		}

		if (!hasSegment)
			Throw("the smooth cubic Bézier command has no coordinates");
	}

	private void ParseQuadraticBezier(char command)
	{
		var isRelative = char.IsLower(command);
		var hasSegment = false;

		while (CanReadNumber())
		{
			var point1 = ReadPoint(isRelative);
			var point2 = ReadPoint(isRelative);
			AddSegment(new SvgQuadraticBezierSegment(point1, point2));
			_current = point2;
			_lastQuadraticControl = point1;
			_lastCubicControl = null;
			_previousCommand = command;
			hasSegment = true;
		}

		if (!hasSegment)
			Throw("the quadratic Bézier command has no coordinates");
	}

	private void ParseSmoothQuadraticBezier(char command)
	{
		var isRelative = char.IsLower(command);
		var hasSegment = false;

		while (CanReadNumber())
		{
			var point1 = IsQuadraticCommand(_previousCommand) && _lastQuadraticControl.HasValue
				? Reflect(_lastQuadraticControl.Value, _current)
				: _current;
			var point2 = ReadPoint(isRelative);
			AddSegment(new SvgQuadraticBezierSegment(point1, point2));
			_current = point2;
			_lastQuadraticControl = point1;
			_lastCubicControl = null;
			_previousCommand = command;
			hasSegment = true;
		}

		if (!hasSegment)
			Throw("the smooth quadratic Bézier command has no coordinates");
	}

	private void ParseArc(char command)
	{
		var isRelative = char.IsLower(command);
		var hasSegment = false;

		while (CanReadNumber())
		{
			var radiusX = Math.Abs(ReadNumber());
			var radiusY = Math.Abs(ReadNumber());
			var rotationAngle = ReadNumber();
			var isLargeArc = ReadFlag();
			var sweepClockwise = ReadFlag();
			var point = ReadPoint(isRelative);
			AddSegment(new SvgArcSegment(
				radiusX,
				radiusY,
				rotationAngle,
				isLargeArc,
				sweepClockwise,
				point));
			_current = point;
			_previousCommand = command;
			ClearControlPoints();
			hasSegment = true;
		}

		if (!hasSegment)
			Throw("the arc command has no coordinates");
	}

	private void CloseFigure()
	{
		if (_figure is null)
			Throw("the close command does not have an open figure");

		_figure!.IsClosed = true;
		_current = _subpathStart;
		_previousCommand = 'Z';
		ClearControlPoints();
		CompleteFigure();
	}

	private SvgPoint ReadPoint(bool isRelative)
	{
		var x = ReadNumber();
		var y = ReadNumber();
		return isRelative
			? new SvgPoint(_current.X + x, _current.Y + y)
			: new SvgPoint(x, y);
	}

	private double ReadNumber()
	{
		SkipSeparators();
		if (IsAtEnd || IsCommand(CurrentCharacter))
			Throw("a number was expected");

		var start = _index;
		if (CurrentCharacter is '+' or '-')
			_index++;

		var hasDigits = false;
		while (!IsAtEnd && char.IsDigit(CurrentCharacter))
		{
			hasDigits = true;
			_index++;
		}

		if (!IsAtEnd && CurrentCharacter == '.')
		{
			_index++;
			while (!IsAtEnd && char.IsDigit(CurrentCharacter))
			{
				hasDigits = true;
				_index++;
			}
		}

		if (!hasDigits)
			Throw("an SVG number was expected");

		if (!IsAtEnd && (CurrentCharacter is 'e' or 'E'))
		{
			_index++;
			if (!IsAtEnd && CurrentCharacter is '+' or '-')
				_index++;

			var exponentStart = _index;
			while (!IsAtEnd && char.IsDigit(CurrentCharacter))
				_index++;

			if (exponentStart == _index)
				Throw("the SVG number has an incomplete exponent");
		}

		var numberText = _data.Substring(start, _index - start);
		if (!double.TryParse(numberText, NumberStyles.Float, CultureInfo.InvariantCulture, out var number))
			Throw($"'{numberText}' is not a valid SVG number");

		return number;
	}

	private bool ReadFlag()
	{
		SkipSeparators();
		if (IsAtEnd || (CurrentCharacter is not '0' and not '1'))
			Throw("an arc flag (0 or 1) was expected");

		return _data[_index++] == '1';
	}

	private bool CanReadNumber()
	{
		SkipSeparators();
		return !IsAtEnd && !IsCommand(CurrentCharacter) &&
			(CurrentCharacter is '+' or '-' or '.' || char.IsDigit(CurrentCharacter));
	}

	private void AddSegment(SvgSegment segment)
	{
		if (_figure is null)
			Throw("a drawing command appeared before a move command");

		_figure!.Segments.Add(segment);
	}

	private void CompleteFigure()
	{
		if (_figure is not null)
		{
			_figures.Add(_figure);
			_figure = null;
		}
	}

	private void ClearControlPoints()
	{
		_lastCubicControl = null;
		_lastQuadraticControl = null;
	}

	private void SkipSeparators()
	{
		while (!IsAtEnd && (char.IsWhiteSpace(CurrentCharacter) || CurrentCharacter == ','))
			_index++;
	}

	private void Throw(string message)
		=> throw new FormatException($"Invalid SVG path at character {_index}: {message}.");

	private static bool IsCommand(char character)
		=> character is 'M' or 'm' or 'L' or 'l' or 'H' or 'h' or 'V' or 'v' or
			'C' or 'c' or 'S' or 's' or 'Q' or 'q' or 'T' or 't' or 'A' or 'a' or
			'Z' or 'z';

	private static bool IsCubicCommand(char command)
		=> command is 'C' or 'c' or 'S' or 's';

	private static bool IsQuadraticCommand(char command)
		=> command is 'Q' or 'q' or 'T' or 't';

	private static SvgPoint Reflect(SvgPoint point, SvgPoint around)
		=> new(2 * around.X - point.X, 2 * around.Y - point.Y);
}
