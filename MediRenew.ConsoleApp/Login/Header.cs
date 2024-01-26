using Spectre.Console;

namespace MediRenew.ConsoleApp.Login;

public class Header
{
    public static void StaticHeader()
    {
        Console.Clear();

        AnsiConsole.Write(new FigletText("MediRenew").Centered().Color(Color.Yellow));

        var rule = new Rule();
        rule.RuleStyle("Hotpink");
        AnsiConsole.Write(rule);

        AnsiConsole.Write(new Rule("\n[Yellow]The Number One - Hospital Database Handler![/]").RuleStyle(Color.HotPink));

        rule = new Rule();
        rule.RuleStyle("Hotpink");
        AnsiConsole.Write(rule);
    }
}
