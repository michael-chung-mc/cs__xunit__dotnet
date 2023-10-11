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
		Color varRes = new Color(argSelf._fieldRed + argOther._fieldRed, argSelf._fieldGreen + argOther._fieldGreen, argSelf._fieldBlue + argOther._fieldBlue);
		// return Color(mbrRed + argOther.mbrRed, mbrGreen + argOther.mbrGreen, mbrBlue + argOther.mbrBlue);
		// Console.WriteLine("public operator+-> varRes(r:" << varRes.mbrRed << ",g:" << varRes.mbrGreen << ",b:" << varRes.mbrBlue;
		// Console.WriteLine(" = this(r:" << mbrRed << ",g:" << mbrGreen << ",b:" << mbrBlue;
		// Console.WriteLine(" + this(r:" << argOther.mbrRed << ",g:" << argOther.mbrGreen << ",b:" << argOther.mbrBlue << std::endl;
		return varRes;
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
	public void RenderConsole()  {
		Console.Write("public renderConsole::Color(");
		Console.Write($"= mbrRed:{_fieldRed}");
		Console.Write($"= mbrGreen:{_fieldGreen}");
		Console.WriteLine($"= mbrBlue:{_fieldBlue}");
	}
};