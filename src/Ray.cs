using LibTuple;
using LibMatrix;
using LibComparinator;
namespace LibRay;

public class Ray {
	public SpaceTuple _fieldOrigin;
	public SpaceTuple _fieldDirection;

	public Ray()
	{
		_fieldOrigin = new Point(0,0,0);
		_fieldDirection = new Vector(0,0,0);
	}

	public Ray(SpaceTuple origin, SpaceTuple direction)
	{
		_fieldOrigin = origin;
		_fieldDirection = direction;
	}
	public Ray(Ray argOther) {
		_fieldOrigin = argOther._fieldOrigin;
		_fieldDirection = argOther._fieldDirection;
	}
	public SpaceTuple GetPosition(double time)
	{
		SpaceTuple varDelta = _fieldDirection * time;
		SpaceTuple varRes = _fieldOrigin + varDelta;
		return varRes;
	}
	public bool CheckEqual(Ray other)
	{
		Comparinator varComp = new Comparinator();
		return varComp.CheckTuple(_fieldOrigin, other._fieldOrigin) && varComp.CheckTuple(_fieldDirection, other._fieldDirection);
	}
	public void ChangeTransform(Matrix matrix)
	{
		_fieldOrigin = matrix * _fieldOrigin;
		_fieldDirection = matrix * _fieldDirection;
		// return new Ray(matrix * _fieldOrigin, matrix * _fieldDirection);
	}
	public Ray GetTransform(Matrix matrix)
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