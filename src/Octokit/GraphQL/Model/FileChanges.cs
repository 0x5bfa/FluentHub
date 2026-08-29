namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A description of a set of changes to a file tree to be made as part of
    /// a git commit, modeled as zero or more file `additions` and zero or more
    /// file `deletions`.
    /// Both fields are optional; omitting both will produce a commit with no
    /// file changes.
    /// `deletions` and `additions` describe changes to files identified
    /// by their path in the git tree using unix-style path separators, i.e.
    /// `/`.  The root of a git tree is an empty string, so paths are not
    /// slash-prefixed.
    /// `path` values must be unique across all `additions` and `deletions`
    /// provided.  Any duplication will result in a validation error.
    /// ### Encoding
    /// File contents must be provided in full for each `FileAddition`.
    /// The `contents` of a `FileAddition` must be encoded using RFC 4648
    /// compliant base64, i.e. correct padding is required and no characters
    /// outside the standard alphabet may be used.  Invalid base64
    /// encoding will be rejected with a validation error.
    /// The encoded contents may be binary.
    /// For text files, no assumptions are made about the character encoding of
    /// the file contents (after base64 decoding).  No charset transcoding or
    /// line-ending normalization will be performed; it is the client's
    /// responsibility to manage the character encoding of files they provide.
    /// However, for maximum compatibility we recommend using UTF-8 encoding
    /// and ensuring that all files in a repository use a consistent
    /// line-ending convention (`\n` or `\r\n`), and that all files end
    /// with a newline.
    /// ### Modeling file changes
    /// Each of the the five types of conceptual changes that can be made in a
    /// git commit can be described using the `FileChanges` type as follows:
    /// 1. New file addition: create file `hello world\n` at path `docs/README.txt`:
    ///        {
    ///          "additions" [
    ///            {
    ///              "path": "docs/README.txt",
    ///              "contents": base64encode("hello world\n")
    ///            }
    ///          ]
    ///        }
    /// 2. Existing file modification: change existing `docs/README.txt` to have new
    ///    content `new content here\n`:
    ///        {
    ///          "additions" [
    ///            {
    ///              "path": "docs/README.txt",
    ///              "contents": base64encode("new content here\n")
    ///            }
    ///          ]
    ///        }
    /// 3. Existing file deletion: remove existing file `docs/README.txt`.
    ///    Note that the path is required to exist -- specifying a
    ///    path that does not exist on the given branch will abort the
    ///    commit and return an error.
    ///        {
    ///          "deletions" [
    ///            {
    ///              "path": "docs/README.txt"
    ///            }
    ///          ]
    ///        }
    /// 4. File rename with no changes: rename `docs/README.txt` with
    ///    previous content `hello world\n` to the same content at
    ///    `newdocs/README.txt`:
    ///        {
    ///          "deletions" [
    ///            {
    ///              "path": "docs/README.txt",
    ///            }
    ///          ],
    ///          "additions" [
    ///            {
    ///              "path": "newdocs/README.txt",
    ///              "contents": base64encode("hello world\n")
    ///            }
    ///          ]
    ///        }
    /// 5. File rename with changes: rename `docs/README.txt` with
    ///    previous content `hello world\n` to a file at path
    ///    `newdocs/README.txt` with content `new contents\n`:
    ///        {
    ///          "deletions" [
    ///            {
    ///              "path": "docs/README.txt",
    ///            }
    ///          ],
    ///          "additions" [
    ///            {
    ///              "path": "newdocs/README.txt",
    ///              "contents": base64encode("new contents\n")
    ///            }
    ///          ]
    ///        }
    /// </summary>
    public class FileChanges
    {
        /// <summary>
        /// Files to delete.
        /// </summary>
        public IEnumerable<FileDeletion> Deletions { get; set; }

        /// <summary>
        /// File to add or change.
        /// </summary>
        public IEnumerable<FileAddition> Additions { get; set; }
    }
}