namespace Octokit.Generators;

internal sealed class SvgIcon
{
	public SvgIcon(
		string identifier,
		string sourceFileName,
		double width,
		double height,
		IReadOnlyList<SvgPath> paths)
	{
		Identifier = identifier;
		SourceFileName = sourceFileName;
		Width = width;
		Height = height;
		Paths = paths;
	}

	public string Identifier { get; }

	public string SourceFileName { get; }

	public double Width { get; }

	public double Height { get; }

	public IReadOnlyList<SvgPath> Paths { get; }
}

internal sealed class SvgPath
{
	public SvgPath(SvgFillRule fillRule, IReadOnlyList<SvgFigure> figures)
	{
		FillRule = fillRule;
		Figures = figures;
	}

	public SvgFillRule FillRule { get; }

	public IReadOnlyList<SvgFigure> Figures { get; }
}

internal enum SvgFillRule
{
	Nonzero,
	EvenOdd,
}

internal sealed class SvgFigure
{
	public SvgFigure(SvgPoint startPoint)
	{
		StartPoint = startPoint;
	}

	public SvgPoint StartPoint { get; }

	public bool IsClosed { get; set; }

	public List<SvgSegment> Segments { get; } = new();
}

internal struct SvgPoint
{
	public SvgPoint(double x, double y)
	{
		X = x;
		Y = y;
	}

	public double X { get; }

	public double Y { get; }
}

internal abstract class SvgSegment;

internal sealed class SvgLineSegment : SvgSegment
{
	public SvgLineSegment(SvgPoint point)
	{
		Point = point;
	}

	public SvgPoint Point { get; }
}

internal sealed class SvgBezierSegment : SvgSegment
{
	public SvgBezierSegment(SvgPoint point1, SvgPoint point2, SvgPoint point3)
	{
		Point1 = point1;
		Point2 = point2;
		Point3 = point3;
	}

	public SvgPoint Point1 { get; }

	public SvgPoint Point2 { get; }

	public SvgPoint Point3 { get; }
}

internal sealed class SvgQuadraticBezierSegment : SvgSegment
{
	public SvgQuadraticBezierSegment(SvgPoint point1, SvgPoint point2)
	{
		Point1 = point1;
		Point2 = point2;
	}

	public SvgPoint Point1 { get; }

	public SvgPoint Point2 { get; }
}

internal sealed class SvgArcSegment : SvgSegment
{
	public SvgArcSegment(
		double radiusX,
		double radiusY,
		double rotationAngle,
		bool isLargeArc,
		bool sweepClockwise,
		SvgPoint point)
	{
		RadiusX = radiusX;
		RadiusY = radiusY;
		RotationAngle = rotationAngle;
		IsLargeArc = isLargeArc;
		SweepClockwise = sweepClockwise;
		Point = point;
	}

	public double RadiusX { get; }

	public double RadiusY { get; }

	public double RotationAngle { get; }

	public bool IsLargeArc { get; }

	public bool SweepClockwise { get; }

	public SvgPoint Point { get; }
}
