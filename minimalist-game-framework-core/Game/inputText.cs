using System;
using System.Collections;

public class inputText
{
	// Get user input as a string of text
	public String text;
	int y;
	public inputText()
	{
		y = 180;
	}
	public String getLatestInput()
	{
		text = text + Engine.TypedText;
		// display text
		
		return text;
	}
	public void drawTextBox()
	{
		// Enter name to save score
		Engine.DrawRectSolid(new Bounds2(800, 70, 400, 100), Theme.getColor());
	}

}


