using System.Numerics;
using LibComparinator;

namespace LibTuple;
public class SpaceTuple {
	public double _fieldX;
	public double _fieldY;
	public double _fieldZ;
	public double _fieldW;
    public SpaceTuple () {
	    _fieldX = 0.0;
	    _fieldY = 0.0;
	    _fieldZ = 0.0;
	    _fieldW = 0.0;
    }
    public SpaceTuple(double argx, double argy, double argz, double argw) {
        _fieldX = argx;
        _fieldY = argy;
        _fieldZ = argz;
        _fieldW = argw;
    }
    public SpaceTuple(SpaceTuple argOther) {
        _fieldX = argOther._fieldX;
        _fieldY = argOther._fieldY;
        _fieldZ = argOther._fieldZ;
        _fieldW = argOther._fieldW;
    }
    public static SpaceTuple operator-(SpaceTuple argSelf) { return new SpaceTuple(-argSelf._fieldX, -argSelf._fieldY, -argSelf._fieldZ, -argSelf._fieldW); }
    public static SpaceTuple operator-(SpaceTuple argSelf, SpaceTuple argOther) { return new SpaceTuple(argSelf._fieldX-argOther._fieldX, argSelf._fieldY-argOther._fieldY, argSelf._fieldZ-argOther._fieldZ, argSelf._fieldW-argOther._fieldW); }
    public static SpaceTuple operator+(SpaceTuple argSelf, SpaceTuple argOther) { return new SpaceTuple(argSelf._fieldX+argOther._fieldX, argSelf._fieldY+argOther._fieldY, argSelf._fieldZ+argOther._fieldZ, argSelf._fieldW+argOther._fieldW); }
    public static SpaceTuple operator*(SpaceTuple argSelf, double multiple) { return new SpaceTuple(argSelf._fieldX * multiple, argSelf._fieldY * multiple, argSelf._fieldZ * multiple, argSelf._fieldW * multiple); }
    public static SpaceTuple operator/(SpaceTuple argSelf, double multiple) { return new SpaceTuple(argSelf._fieldX / multiple, argSelf._fieldY / multiple, argSelf._fieldZ / multiple, argSelf._fieldW / multiple); }
    public double GetMagnitude() { return Math.Sqrt(_fieldX * _fieldX + _fieldY * _fieldY + _fieldZ * _fieldZ + _fieldW * _fieldW); }
    public double GetDotProduct(SpaceTuple argOther) { return (_fieldX * argOther._fieldX) + (_fieldY * argOther._fieldY) + (_fieldZ * argOther._fieldZ) + (_fieldW * argOther._fieldW); }
    public SpaceTuple GetNormal()
    {
    	Comparinator varComp = new Comparinator();
    	double varMagnitude = GetMagnitude();
    	return varComp.CheckFloat(varMagnitude, 0) ? new Vector(0.0,0.0,0.0) : new Vector(_fieldX/varMagnitude, _fieldY/varMagnitude, _fieldZ/varMagnitude);
    }
    public SpaceTuple GetCrossProduct(SpaceTuple argOther)
    {
    	return new Vector(_fieldY * argOther._fieldZ - _fieldZ * argOther._fieldY, _fieldZ * argOther._fieldX - _fieldX * argOther._fieldZ, _fieldX * argOther._fieldY - _fieldY * argOther._fieldX);
    }
    public SpaceTuple GetReflect(SpaceTuple argNormal) { 
        return new Vector(this._fieldX, this._fieldY, this._fieldZ) - (argNormal * 2.0 * GetDotProduct(argNormal));
    }
    bool CheckEqual(SpaceTuple argOther)
    {
        Comparinator varComp = new Comparinator ();
        return varComp.CheckTuple(this, argOther);
    }
    public virtual void RenderConsole() {
        Console.WriteLine($"Tuple::RenderConsole()::{{mbrX:{_fieldX}, mbrY:{_fieldY}, mbrZ:{_fieldZ}, mbrW:{_fieldW}}}");
    }
    public void SetPoints (double argX, double argY, double argZ, double argW) {
        _fieldX = argX;
        _fieldY = argY;
        _fieldZ = argZ;
        _fieldW = argW;
    }
    public void SetAdd (SpaceTuple argOther) {
        _fieldX += argOther._fieldX;
        _fieldY += argOther._fieldY;
        _fieldZ += argOther._fieldZ;
        _fieldW += argOther._fieldW;
    }
    public void SetSubtract (SpaceTuple argOther) {
        _fieldX -= argOther._fieldX;
        _fieldY -= argOther._fieldY;
        _fieldZ -= argOther._fieldZ;
        _fieldW -= argOther._fieldW;
    }
    public void SetNegative()
    {
        _fieldX = -_fieldX;
        _fieldY = -_fieldY;
        _fieldZ = -_fieldZ;
        _fieldW = -_fieldW;
    }
}
public class Point : SpaceTuple {
    public Point() {
	    _fieldX = 0.0;
	    _fieldY = 0.0;
	    _fieldZ = 0.0;
	    _fieldW = 1.0;
    }
    public Point(SpaceTuple argOther) {
        _fieldX = argOther._fieldX;
        _fieldY = argOther._fieldY;
        _fieldZ = argOther._fieldZ;
        _fieldW = 1.0;
    }
    public Point(double argX, double argY, double argZ) {
	    _fieldX = argX;
	    _fieldY = argY;
	    _fieldZ = argZ;
	    _fieldW = 1.0;
    }
    public void SetPoints (double argX, double argY, double argZ) {
        _fieldX = argX;
        _fieldY = argY;
        _fieldZ = argZ;
    }
}
public class Vector : SpaceTuple {
    public Vector() {
	    _fieldX = 0.0;
	    _fieldY = 0.0;
	    _fieldZ = 0.0;
	    _fieldW = 0.0;
    }
    public Vector(SpaceTuple argOther) {
        _fieldX = argOther._fieldX;
        _fieldY = argOther._fieldY;
        _fieldZ = argOther._fieldZ;
        _fieldW = 0.0;
    }
    public Vector(double argX, double argY, double argZ) {
	    _fieldX = argX;
	    _fieldY = argY;
	    _fieldZ = argZ;
	    _fieldW = 0.0;
    }
    public void SetPoints (double argX, double argY, double argZ) {
        _fieldX = argX;
        _fieldY = argY;
        _fieldZ = argZ;
    }
}
