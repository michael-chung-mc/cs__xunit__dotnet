using LibTuple;
using LibMatrix;
using LibComparinator;
namespace LibRay;

public class Ray {
	public Point _fieldOrigin;
	public Vector _fieldDirection;

	public Ray()
	{
		_fieldOrigin = new Point(0,0,0);
		_fieldDirection = new Vector(0,0,0);
	}

	public Ray(Point origin, Vector direction)
	{
		_fieldOrigin = origin;
		//Console.WriteLine(origin.x);
		_fieldDirection = direction;
	}
	public Ray(Ray argOther) {
		_fieldOrigin = argOther._fieldOrigin;
		_fieldDirection = argOther._fieldDirection;
	}
	// public static operator=(Ray argOther) {
	// 	_fieldOrigin = argOther._fieldOrigin;
	// 	_fieldDirection = argOther._fieldDirection;
	// }
	public Point GetPosition(double time)
	{
		// Console.WriteLine("public getPosition()";
		// Console.WriteLine(" + public _fieldDirection(x:" << _fieldDirection._fieldX << ",y:" << _fieldDirection._fieldY << ",z:" << _fieldDirection._fieldZ << ",w:" << _fieldDirection._fieldW;
		// Console.WriteLine(" * " << time);
		Vector varDelta = _fieldDirection * time;
		// Console.WriteLine("+ public varDelta(x:" << varDelta._fieldX << ",y:" << varDelta._fieldY << ",z:" << varDelta._fieldZ << ",w:" << varDelta._fieldW);
		// Console.WriteLine("+ public mbOrigin(x:" << _fieldOrigin._fieldX << ",y:" << _fieldOrigin._fieldY << ",z:" << _fieldOrigin._fieldZ << ",w:" << _fieldOrigin._fieldW);
		Point varRes = _fieldOrigin + varDelta;
		// return _fieldOrigin + _fieldDirection * time;
		// Console.WriteLine("public getPosition()->varRes(x:" << varRes._fieldX << ",y:" << varRes._fieldY << ",z:" << varRes._fieldZ << ",w:" << varRes._fieldW);
		return varRes;
	}
	public bool CheckEqual(Ray other)
	{
		Comparinator varComp = new Comparinator();
		return varComp.CheckTuple(_fieldOrigin, other._fieldOrigin) && varComp.CheckTuple(_fieldDirection, other._fieldDirection);
	}
	public Ray Transform(Matrix matrix)
	{
		return new Ray(matrix * _fieldOrigin, matrix * _fieldDirection);
	}
	public Ray Transform(TranslationMatrix matrix)
	{
		return new Ray(matrix * _fieldOrigin, matrix * _fieldDirection);
	}
	public Ray Transform(ScalingMatrix matrix)
	{
		return new Ray(matrix * _fieldOrigin, matrix * _fieldDirection);
	}
	public void RenderConsole() {
		Console.WriteLine("Ray::renderConsole()");
		Console.WriteLine("Ray::renderConsole()::_fieldOrigin(");
		_fieldOrigin.RenderConsole();
		Console.WriteLine("Ray::renderConsole()::_fieldDirection(");
		_fieldDirection.RenderConsole();
	}
};