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
		_fieldDirection = direction;
	}
	public Ray(Ray argOther) {
		_fieldOrigin = argOther._fieldOrigin;
		_fieldDirection = argOther._fieldDirection;
	}
	public Point GetPosition(double time)
	{
		Vector varDelta = _fieldDirection * time;
		Point varRes = _fieldOrigin + varDelta;
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
	public void RenderConsole() {
		Console.WriteLine("Ray::renderConsole()");
		Console.WriteLine("Ray::renderConsole()::_fieldOrigin(");
		_fieldOrigin.RenderConsole();
		Console.WriteLine("Ray::renderConsole()::_fieldDirection(");
		_fieldDirection.RenderConsole();
	}
};