using LibTuple;
using LibMatrix;
using LibRay;
using LibComparinator;
using LibIntersection;
using LibMaterial;
using LibColor;
using LibLight;
using LibProjectMeta;
namespace LibForm;

public class Form {
	public Point _fieldOrigin;
	public double _fieldRadius;
	public Matrix _fieldTransform;
	public Matrix _fieldTransformInverse;
	public Material _fieldMaterial;
	public Ray _fieldObjectRay;
	public Form()
	{
		_fieldOrigin = new Point(0, 0, 0);
		_fieldRadius = 0;
		SetTransform(new IdentityMatrix(4, 4));
		_fieldMaterial = new Material();
		_fieldObjectRay = new Ray();
	}
	public Form(Form argOther)
	{
		_fieldOrigin = argOther._fieldOrigin;
		_fieldRadius = argOther._fieldRadius;
		// _fieldTransform = new Matrix>(other._fieldTransform);
		SetTransform(argOther._fieldTransform);
		SetMaterial(argOther._fieldMaterial);
		_fieldObjectRay = argOther._fieldObjectRay;
	}
	// public Form(Form other) {
	// 	_fieldOrigin = other._fieldOrigin;
	// 	_fieldRadius = other._fieldRadius;
	// 	_fieldTransform = other._fieldTransform;
	// 	_fieldTransformInverse = other._fieldTransformInverse;
	// 	_fieldMaterial = other._fieldMaterial;
	// 	_fieldObjectRay = other._fieldObjectRay;
	// }
// 	Form operator=(new Form other) {
	// 	if (this == other) return this;
	// 	_fieldOrigin = other._fieldOrigin;
	// 	_fieldRadius = other._fieldRadius;
	// 	setTransform(other._fieldTransform);
	// 	setMaterial(other._fieldMaterial);
	// 	return this;
	// }
// 	Form operator=(Form other) noexcept {
	// 	if (this == other) return this;
	// 	_fieldOrigin = other._fieldOrigin;
	// 	_fieldRadius = other._fieldRadius;
	// 	_fieldTransform = new other._fieldTransform);
	// 	other._fieldTransform = nullptr;
	// 	_fieldTransformInverse = new other._fieldTransformInverse);
	// 	other._fieldTransformInverse = nullptr;
	// 	_fieldMaterial = new other._fieldMaterial);
	// 	other._fieldMaterial = nullptr;
	// 	return this;
	// }
	public Intersections GetIntersections(Ray argRay)
	{
		// Console.WriteLine("default get intersections";
		// _fieldObjectRay = argRay.transform((_fieldTransform.invert()));
		this._fieldObjectRay = argRay.Transform(this._fieldTransformInverse);
		return GetIntersectionsLocal(_fieldObjectRay);
	}
	public virtual Intersections GetIntersectionsLocal(Ray argRay)
	{
		return new Intersections();
	}
	public bool CheckEqual(Form argOther)
	{
		Comparinator ce = new Comparinator();
		return ce.CheckTuple(_fieldOrigin, argOther._fieldOrigin) && _fieldTransform.CheckEqual(argOther._fieldTransform) && _fieldMaterial.CheckEqual(argOther._fieldMaterial) && _fieldObjectRay.CheckEqual(argOther._fieldObjectRay);
	}
	public Vector GetNormal(Point argPoint)
	{
		// Point varObjectPoint = (_fieldTransform.invert())  argPoint;
		Point varObjectPoint = _fieldTransformInverse * argPoint;
		Vector varObjectNormal = GetNormalLocal(varObjectPoint);
		// Matrix varTransform = (_fieldTransform.invert()).transpose();
		// Matrix varTransform = (_fieldTransformInverse.transpose());
		Matrix varTransform = _fieldTransformInverse.GetTranspose();
		Vector varWorldNormal = varTransform * varObjectNormal;
		varWorldNormal._fieldW = 0;
		return varWorldNormal.GetNormal();
	}
	public Vector GetNormalLocal(Point argPoint)
	{
		return new Vector(argPoint._fieldX,argPoint._fieldY,argPoint._fieldZ);
	}
	public Color GetColor(PointSource argLighting, Point argPosition, Vector argEye, Vector argNormal, bool argInShadow)
	{
		// Console.WriteLine("public getColor()");
		// Point varObjP = (_fieldTransform.invert())  argPosition;
		Point varObjP = (this._fieldTransformInverse) * argPosition;
		// Console.WriteLine("public getColor()::varObjP(x:" << varObjP._fieldX << ",y:" << varObjP._fieldY << ",z:" << varObjP._fieldZ << ",w:" << varObjP._fieldW << ")");
		// Console.WriteLine("= inverse(_fieldTransform(");
		// _fieldTransform.renderConsole();
		// Console.WriteLine("=");
		// _fieldTransform.invert().renderConsole();
		// Console.WriteLine("  argPosition(x:" << argPosition._fieldX << ",y:" << argPosition._fieldY << ",z:" << argPosition._fieldZ << ",w:" << argPosition._fieldW);
		// Point varPatternP = (_fieldMaterial._fieldPattern._fieldTransform.invert())  varObjP;
		Point varPatternP = (this._fieldMaterial.mbrPattern._fieldTransformInverse) * varObjP;
		// Console.WriteLine("public getColor()::varPatternP(x:" << varPatternP._fieldX << ",y:" << varPatternP._fieldY << ",z:" << varPatternP._fieldZ << ",w:" << varPatternP._fieldW << ")");
		// Console.WriteLine("= inverse(_fieldTransform(");
		// _fieldMaterial._fieldPattern._fieldTransform.renderConsole();
		// Console.WriteLine("=");
		// _fieldMaterial._fieldPattern._fieldTransform.invert().renderConsole();
		// Console.WriteLine(" varObjP");
		Color varRes = this._fieldMaterial.GetColor(argLighting, varPatternP, argEye, argNormal, argInShadow);
		// Color varRes = _fieldMaterial.getColor(argLighting, argPosition, argEye, argNormal, argInShadow);
		// Console.WriteLine("public getColor().varRes(r:" << varRes._fieldRed << "g:" << varRes._fieldGreen << "b:" << varRes._fieldBlue);
		return varRes;
		// return _fieldMaterial.getColor(argLighting, varPatternP, argEye, argNormal, argInShadow);
	}
	// Color getColorShaded(new PointSource argLighting, new Point argPosition, new Vector argEye, new Vector argNormal, bool argInShadow) {
	// 	return _fieldMaterial.getColor(argLighting, argPosition, argEye, argNormal, argInShadow);
	// }
	public Color GetColorLocal(Point argPosition)
	{
		// Point varObjP = (_fieldTransform.invert())  argPosition;
		// Point varPatternP = (_fieldMaterial._fieldPattern._fieldTransform.invert())  varObjP;
		Point varObjP = this._fieldTransformInverse * argPosition;
		Point varPatternP = this._fieldMaterial.mbrPattern._fieldTransformInverse * varObjP;
		return this._fieldMaterial.mbrPattern.GetColorLocal(varPatternP);
	}
	public void SetTransform(Matrix argMatrix) {
		this._fieldTransform = argMatrix;
		this._fieldTransformInverse = argMatrix.GetInverse();
	}
	public void SetMaterial(Material argMaterial) {
		this._fieldMaterial = argMaterial;
	}
	public void RenderConsole() {
		Console.WriteLine("public renderConsole()");
		Console.WriteLine("public _fieldOrigin: ");
		_fieldOrigin.RenderConsole();
		Console.WriteLine($"public _fieldRadius:{_fieldRadius}");
		Console.WriteLine("public _fieldTransform:");
		_fieldTransform.RenderConsole();
		Console.WriteLine("public _fieldMaterial: ");
		_fieldMaterial.RenderConsole();
		Console.WriteLine("public _fieldObjectRay: ");
		_fieldObjectRay.RenderConsole();
	}
};

public class Sphere : Form {
	public Sphere()
	{
		_fieldRadius = 1.0;
	}
	public override Intersections GetIntersectionsLocal(Ray argRay)
	{
		// Console.WriteLine("sphere get intersections";
		Intersections varIntersections = new Intersections();
		Vector sphereToRay = argRay._fieldOrigin - (new Point(0, 0, 0));
		double a = argRay._fieldDirection.GetDotProduct(argRay._fieldDirection);
		double b = 2 * argRay._fieldDirection.GetDotProduct(sphereToRay);
		double c = sphereToRay.GetDotProduct(sphereToRay) - 1;
		double discriminant = (b*b) - 4 * a * c;
		if (discriminant < 0) { return varIntersections; }
		double intersectOne = (-b - Math.Sqrt(discriminant)) / (2 * a);
		double intersectTwo = (-b + Math.Sqrt(discriminant)) / (2 * a);
		// if (intersectOne < intersectTwo)
		// {
		// 	// varIntersections.intersect(intersectOne, new Sphere>(this));
		// 	// varIntersections.intersect(intersectTwo, new Sphere>(this));
		// 	varIntersections.intersect(intersectOne, this);
		// 	varIntersections.intersect(intersectTwo, this);
		// }
		// else
		// {
		// 	// varIntersections.intersect(intersectTwo, new Sphere>(this));
		// 	// varIntersections.intersect(intersectOne, new Sphere>(this));
		// 	varIntersections.intersect(intersectTwo, this);
		// 	varIntersections.intersect(intersectOne, this);
		// }
		varIntersections.SetIntersect(intersectOne, this);
		varIntersections.SetIntersect(intersectTwo, this);
		return varIntersections;
	}
	public Vector GetNormalLocal(Point argPoint)
	{
		// Point varObjectPoint = (_fieldTransform.invert())  argPoint;
		Vector varObjectNormal = argPoint - _fieldOrigin;
		return varObjectNormal.GetNormal();
	}
	// Vector Sphere::getNormal(Point argPoint)
	// {
	// 	// return (argPoint-origin).normalize();
	// 	Point varObjectPoint = (_fieldTransform.invert())  argPoint;
	// 	Vector varObjectNormal = varObjectPoint - _fieldOrigin;
	// 	Matrix varTransform = (_fieldTransform.invert()).transpose();
	// 	Vector varWorldNormal = varTransform  varObjectNormal;
	// 	varWorldNormal._fieldW = 0;
	// 	return varWorldNormal.normalize();
	// }
	public bool CheckEqual(Form other)
	{
		Comparinator ce = new Comparinator();
		return ce.CheckTuple(_fieldOrigin, other._fieldOrigin) && _fieldRadius == other._fieldRadius && _fieldTransform.CheckEqual(other._fieldTransform) && _fieldMaterial.CheckEqual(other._fieldMaterial);
	}
};

public class SphereGlass : Sphere {
	public SphereGlass() {
		this._fieldMaterial.mbrTransparency = 1.0;
		this._fieldMaterial.mbrRefractiveIndex = 1.5;
	}
	public SphereGlass(SphereGlass argOther) {
		this._fieldMaterial = argOther._fieldMaterial;
	}
};

public class Plane : Form {
	public Vector GetNormalLocal(Point argPoint)
	{
		return new Vector(0, 1, 0);
	}
	public override Intersections GetIntersectionsLocal(Ray argRay)
	{
		ProjectMeta varPM = new ProjectMeta();
		// Console.WriteLine("plane get intersections";
		if (Math.Abs(argRay._fieldDirection._fieldY) <= varPM.getEpsilon())
		{
			return new Intersections();
		}
		double varTime = -argRay._fieldOrigin._fieldY / argRay._fieldDirection._fieldY;
		// return Intersections(varTime, new Plane>(this));
		return new Intersections(varTime, this);
	}
};