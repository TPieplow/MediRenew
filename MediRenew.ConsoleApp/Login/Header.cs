using Spectre.Console;

namespace MediRenew.ConsoleApp.Login;

public class Header
{
    public static void StaticHeader()
    {
        Console.Clear();
        DateTime now = DateTime.Now;
        DateOnly today = DateOnly.FromDateTime(now);
        var hospital = Emoji.Known.Hospital;

        //AnsiConsole.Write(today.ToString()).Color(Color.Yellow).Centered());

        AnsiConsole.Write(new FigletText("MediRenew" + hospital).Centered().Color(Color.Yellow));

        var rule = new Rule();
        rule.RuleStyle("Hotpink");
        AnsiConsole.Write(rule);

        AnsiConsole.Write(new Rule("\n[Yellow]The Number One - Hospital Database Handler![/]").RuleStyle(Color.HotPink));

        rule = new Rule();
        rule.RuleStyle("Hotpink");
        AnsiConsole.Write(rule);
    }
}
