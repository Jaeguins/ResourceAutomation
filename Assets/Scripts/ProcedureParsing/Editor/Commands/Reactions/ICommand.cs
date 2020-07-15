namespace ProcedureParsing.Commands {

    public interface ICommand {
        CommandProcess Reaction { get; }
        CommandProcess Validation { get; }
    }

}