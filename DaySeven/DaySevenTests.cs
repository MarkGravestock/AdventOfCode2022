using System.Text;
using FluentAssertions;

namespace DaySeven;

public class DaySevenTests
{
    [Fact]
    public void it_can_create_a_sub_directory_from_commands()
    {
        var fileSystem = new FileSystem();
        fileSystem.CreateFromInteractions(new[]
        {
            "$ cd /",
            "$ ls",
            "dir a"
        });

        fileSystem.ToString().Should().ContainEquivalentOf(
            "- / (dir)" +
            "- a (dir)"
        );
    }

    [Fact]
    public void it_can_create_files_and_subdirectories_from_commands()
    {
        var fileSystem = new FileSystem();
        fileSystem.CreateFromInteractions(new[]
        {
            "$ cd /",
            "$ ls",
            "dir a",
            "14848514 b.txt",
            "8504156 c.dat",
            "dir d"
        });

        fileSystem.ToString().Should().BeEquivalentTo(
            "- / (dir)" +
              "- a (dir)" +
              "- b.txt (file, size=14848514)" +
              "- c.dat (file, size=8504156)" +
              "- d (dir)"
        );
    }

    [Fact]
    public void it_can_change_into_a_subdirectory()
    {
        var fileSystem = new FileSystem();
        fileSystem.CreateFromInteractions(new[]
        {
            "$ cd /",
            "$ ls",
            "dir a",
            "14848514 b.txt",
            "8504156 c.dat",
            "dir d",
            "$ cd a",
            "$ ls",
            "dir e",
            "29116 f",
            "2557 g",
            "62596 h.lst"
        });

        fileSystem.ToString().Should().ContainEquivalentOf(
            "- / (dir)" +
                  "- a (dir)" +
                  "- e (dir)" +
                  "- f (file, size=29116)" +
                  "- g (file, size=2557)" +
                  "- h.lst (file, size=62596)" +
                  "- b.txt (file, size=14848514)" +
                  "- c.dat (file, size=8504156)" +
                  "- d (dir)"
        );
    }

    [Fact] public void it_can_calculate_the_total_size_of_all_directories()
    {
        var fileSystem = new FileSystem();
        fileSystem.CreateFromInteractions(new[]
        {
            "$ cd /",
            "$ ls",
            "dir a",
            "14848514 b.txt",
            "8504156 c.dat",
            "dir d",
            "$ cd a",
            "$ ls",
            "dir e",
            "29116 f",
            "2557 g",
            "62596 h.lst",
            "$ cd e",
            "$ ls",
            "584 i",
            "$ cd ..",
            "$ cd ..",
            "$ cd d",
            "$ ls",
            "4060174 j",
            "8033020 d.log",
            "5626152 d.ext",
            "7214296 k"
        });

        fileSystem.Size().Should().Be(48381165);
    }

    public class FileSystem
    {
        private Directory rootDirectory = new("/", null);

        private Directory currentDirectory = null;
        private Directory parentDirectory = null;

        public int Size()
        {
            return rootDirectory.Size();
        }

        public void CreateFromInteractions(IEnumerable<string> interactions)
        {
            foreach (var interaction in interactions)
            {
                if (interaction.StartsWith("$"))
                {
                    ProcessCommand(interaction.Substring(2));
                    continue;
                }

                ProcessResponse(interaction);
            }
        }

        private void ProcessResponse(string interaction)
        {
            if (interaction.StartsWith("dir"))
            {
                AddDirectory(interaction.Substring(4));
                return;
            }

            AddFile(interaction);
        }

        private void AddFile(string interaction)
        {
            var tokens = interaction.Split(" ");
            var fileSize = int.Parse(tokens[0]);
            var fileName = tokens[1];

            currentDirectory.AddFile(fileSize, fileName);
        }

        private void AddDirectory(string directoryName)
        {
            currentDirectory.AddDirectory(directoryName, currentDirectory);
        }

        private void ProcessCommand(string commandText)
        {
            if (commandText.StartsWith("cd"))
            {
                ChangeDirectory(commandText.Substring(3));
            }
        }

        private void ChangeDirectory(string directoryExpression)
        {
            if (directoryExpression == "/")
            {
                currentDirectory = rootDirectory;
                return;
            }

            if (directoryExpression == "..")
            {
                currentDirectory = currentDirectory.ParentDirectory();
                return;
            }

            currentDirectory = currentDirectory.FindNamedDirectory(directoryExpression);
        }

        public override string ToString()
        {
            return rootDirectory.ToString();
        }
    }

    public class Directory : IDirectoryContent
    {
        private readonly string _directoryName;
        private readonly Directory _parentDirectory;

        private List<IDirectoryContent> contents = new();

        internal Directory(string directoryName, Directory parentDirectory)
        {
            _directoryName = directoryName;
            _parentDirectory = parentDirectory;
        }

        public void AddDirectory(string directoryName, Directory parentDirectory)
        {
            contents.Add(new Directory(directoryName, parentDirectory));
        }

        public string Name()
        {
            return _directoryName;
        }

        public int Size()
        {
            return contents.Sum(x => x.Size());
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append($"- {_directoryName} (dir)");

            contents.ForEach(content => builder.Append($"{content.ToString()}"));

            return builder.ToString();
        }

        public void AddFile(int fileSize, string fileName)
        {
            contents.Add(new File(fileSize, fileName));
        }

        public Directory FindNamedDirectory(string name)
        {
            return (Directory)contents.First(item => item.Name() == name);
        }

        public Directory ParentDirectory()
        {
            return _parentDirectory;
        }
    }

    internal class File : IDirectoryContent
    {
        private readonly int _fileSize;
        private readonly string _fileName;

        public File(int fileSize, string fileName)
        {
            _fileSize = fileSize;
            _fileName = fileName;
        }

        public string Name()
        {
            return _fileName;
        }

        public int Size()
        {
            return _fileSize;
        }

        public override string ToString()
        {
            return $"- {_fileName} (file, size={_fileSize})";
        }
    }

    internal interface IDirectoryContent
    {
        public string Name();
        public int Size();
    }
}