using System;

abstract class Unit
{
    public string Title;
    public string ShortTitle;
    public string TitleParrental;
    public string ShortTitleParrental;

    protected Unit(string title, string shortTitle, string titleParrental, string shortTitleParrental)
    {
        Title = title;
        ShortTitle = shortTitle;
        TitleParrental = titleParrental;
        ShortTitleParrental = shortTitleParrental;
    }
}
