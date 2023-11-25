using LibTuple;
using LibComparinator;
namespace LibColor;

public class Color : SpaceTuple {
	public double _fieldRed;
	public double _fieldGreen;
	public double _fieldBlue;
	public Color()
	{
		_fieldRed = 0;
		_fieldGreen = 0;
		_fieldBlue = 0;
	}
	public Color(double argRed, double argGreen, double argBlue)
	{
		_fieldRed = argRed;
		_fieldGreen = argGreen;
		_fieldBlue = argBlue;
	}

	public static Color operator+(Color argSelf, Color argOther) {
		return new Color(argSelf._fieldRed + argOther._fieldRed, argSelf._fieldGreen + argOther._fieldGreen, argSelf._fieldBlue + argOther._fieldBlue);
	}
	public static Color operator-(Color argSelf, Color argOther) {
		return new Color(argSelf._fieldRed - argOther._fieldRed, argSelf._fieldGreen - argOther._fieldGreen, argSelf._fieldBlue - argOther._fieldBlue);
	}
	public static Color operator*(Color argSelf, double argMultiple) {
		return new Color(argSelf._fieldRed * argMultiple, argSelf._fieldGreen * argMultiple, argSelf._fieldBlue * argMultiple);
	}
	public static Color operator*(Color argSelf, Color argOther) {
		return new Color(argSelf._fieldRed * argOther._fieldRed, argSelf._fieldGreen * argOther._fieldGreen, argSelf._fieldBlue * argOther._fieldBlue);
	}
	public bool CheckEqual(Color argOther) 
	{
		Comparinator ce = new Comparinator();
		return ce.CheckFloat(_fieldRed,argOther._fieldRed) && ce.CheckFloat(_fieldGreen,argOther._fieldGreen) && ce.CheckFloat(_fieldBlue,argOther._fieldBlue);
	}
	public override void RenderConsole()  {
		Console.Write("public renderConsole::Color(");
		Console.Write($"= mbrRed:{_fieldRed}");
		Console.Write($"= mbrGreen:{_fieldGreen}");
		Console.WriteLine($"= mbrBlue:{_fieldBlue}");
	}
};