using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime.InteropServices;

abstract class Browser
{
    private string open_script;
    private string close_script;
    private string terminal;

    public abstract void openBrowser();
    public abstract void closeBrowser();
}

class WindowsBrowser : Browser
{
    private string open_script = "/C start firefox /new-window";
    private string close_script = "/C taskkill /F /IM firefox.exe /";
    private string terminal = "CMD.exe";

    public WindowsBrowser()
    {
        Console.WriteLine("Открывается браузер под Windows.");
    }

    public override void openBrowser()
    {
        System.Diagnostics.Process.Start(terminal, open_script);
    }

    public override void closeBrowser()
    {
        System.Diagnostics.Process.Start(terminal, close_script);
    }
}

class MacOSBrowser : Browser
{
    private string open_script = "open -a Firefox.app";
    private string close_script = "pkill -f Firefox.app";
    private string terminal = "/bin/zsh";

    public MacOSBrowser()
    {
        Console.WriteLine("Открывается браузер под MacOS.");
    }

    public override void openBrowser()
    {
        System.Diagnostics.Process.Start(terminal, open_script);
    }

    public override void closeBrowser()
    {
        System.Diagnostics.Process.Start(terminal, close_script);
    }
}

class LinuxBrowser : Browser
{
    private string open_script = "firefox --new-tab";
    private string close_script = "pkill -f firefox";
    private string terminal = "/bin/bash";

    public LinuxBrowser()
    {
        Console.WriteLine("Открывается браузер под GNU/Linux.");
    }

    public override void openBrowser()
    {
        System.Diagnostics.Process.Start(terminal, open_script);
    }

    public override void closeBrowser()
    {
        System.Diagnostics.Process.Start(terminal, close_script);
    }
}

abstract class BrowserFactory
{
    public string name { get; set; }

    public BrowserFactory(string input_name)
    {
        name = input_name;
    }

    abstract public Browser Create();
}

class WindowsBrowserFactory : BrowserFactory
{
    public WindowsBrowserFactory(string input_name) : base(input_name)
    {
    }

    public override Browser Create()
    {
        return new WindowsBrowser();
    }
}

class MacOSBrowserFactory : BrowserFactory
{
    public MacOSBrowserFactory(string input_name) : base(input_name)
    {
    }

    public override Browser Create()
    {
        return new MacOSBrowser();
    }
}

class LinuxBrowserFactory : BrowserFactory
{
    public LinuxBrowserFactory(string input_name) : base(input_name)
    {
    }

    public override Browser Create()
    {
        return new LinuxBrowser();
    }
}

class Program
{
    public static void Main()
    {
        BrowserFactory browser_factory = new MacOSBrowserFactory("Открыватель браузиров под макасось.");
        Browser browser1 = browser_factory.Create();
        browser1.openBrowser();

        /*
        browser_factory = new WindowsBrowserFactory("Открыватель браузиров шиндавс.");
        Browser browser2 = browser_factory.Create();
        browser2.openBrowser();

        browser_factory = new WindowsBrowserFactory("Открыватель браузиров глинукс.");
        Browser browser3 = browser_factory.Create();
        browser2.openBrowser();
        */

        Console.ReadLine();
    }
}
