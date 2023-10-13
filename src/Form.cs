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
	public bool _fieldCastsShadow;
	public Point _fieldOrigin;
	public double _fieldRadius;
	public Matrix _fieldTransform;
	public Matrix _fieldTransformInverse;
	public Material _fieldMaterial;
	public Ray _fieldObjectRay;
	public Form()
	{
		_fieldCastsShadow = true;
		_fieldOrigin = new Point(0, 0, 0);
		_fieldRadius = 0;
		SetTransform(new IdentityMatrix(4, 4));
		_fieldMaterial = new Material();
		_fieldObjectRay = new Ray();
	}
	public Form(Form argOther)
	{
		_fieldCastsShadow = true;
		_fieldOrigin = argOther._fieldOrigin;
		_fieldRadius = argOther._fieldRadius;
		SetTransform(argOther._fieldTransform);
		SetMaterial(argOther._fieldMaterial);
		_fieldObjectRay = argOther._fieldObjectRay;
	}
	public Intersections GetIntersections(Ray argRay)
	{
		this._fieldObjectRay = argRay.Transform(this._fieldTransformInverse);
		return GetIntersectionsLocal(_fieldObjectRay);
	}
	public virtual Intersections GetIntersectionsLocal(Ray argRay)
	{
		return new Intersections();
	}
	public virtual bool CheckEqual(Form argOther)
	{
		Comparinator ce = new Comparinator();
		return ce.CheckTuple(_fieldOrigin, argOther._fieldOrigin)
			&& _fieldTransform.CheckEqual(argOther._fieldTransform)
			&& _fieldMaterial.CheckEqual(argOther._fieldMaterial)
			&& _fieldObjectRay.CheckEqual(argOther._fieldObjectRay)
			&& _fieldCastsShadow == argOther._fieldCastsShadow;
	}
	public Vector GetNormal(Point argPoint)
	{
		Point varObjectPoint = _fieldTransformInverse * argPoint;
		Vector varObjectNormal = GetNormalLocal(varObjectPoint);
		Matrix varTransform = _fieldTransformInverse.GetTranspose();
		Vector varWorldNormal = varTransform * varObjectNormal;
		varWorldNormal._fieldW = 0;
		return varWorldNormal.GetNormal();
	}
	public virtual Vector GetNormalLocal(Point argPoint)
	{
		return new Vector(argPoint._fieldX,argPoint._fieldY,argPoint._fieldZ);
	}
	public Color GetColor(PointSource argLighting, Point argPosition, Vector argEye, Vector argNormal, bool argInShadow)
	{
		Point varObjP = (this._fieldTransformInverse) * argPosition;
		Point varPatternP = (this._fieldMaterial._fieldPattern._fieldTransformInverse) * varObjP;
		Color varRes = this._fieldMaterial.GetColor(argLighting, varPatternP, argEye, argNormal, argInShadow);
		// Color varRes = _fieldMaterial.GetColor(argLighting, argPosition, argEye, argNormal, argInShadow);
		return varRes;
	}
	public Color GetColorShaded(PointSource argLighting, Point argPosition, Vector argEye, Vector argNormal, bool argInShadow)
	{
		Color varRes = _fieldMaterial.GetColor(argLighting, argPosition, argEye, argNormal, argInShadow);
		return varRes;
	}
	public Color GetColorLocal(Point argPosition)
	{
		Point varObjP = this._fieldTransformInverse * argPosition;
		Point varPatternP = this._fieldMaterial._fieldPattern._fieldTransformInverse * varObjP;
		return this._fieldMaterial._fieldPattern.GetColor(varPatternP);
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
		Intersections varIntersections = new Intersections();
		Vector sphereToRay = argRay._fieldOrigin - new Point(0, 0, 0);
		double a = argRay._fieldDirection.GetDotProduct(argRay._fieldDirection);
		double b = 2 * argRay._fieldDirection.GetDotProduct(sphereToRay);
		double c = sphereToRay.GetDotProduct(sphereToRay) - 1;
		double discriminant = (b*b) - (4 * a * c);
		if (discriminant < 0) { return varIntersections; }
		double intersectOne = (-b - Math.Sqrt(discriminant)) / (2 * a);
		double intersectTwo = (-b + Math.Sqrt(discriminant)) / (2 * a);
		varIntersections.SetIntersect(intersectOne, this);
		varIntersections.SetIntersect(intersectTwo, this);
		return varIntersections;
	}
	public override Vector GetNormalLocal(Point argPoint)
	{
		Vector varObjectNormal = argPoint - _fieldOrigin;
		return varObjectNormal.GetNormal();
	}
	public override bool CheckEqual(Form argOther)
	{
		Comparinator ce = new Comparinator();
		return ce.CheckFloat(_fieldRadius, argOther._fieldRadius)
			&& ce.CheckTuple(_fieldOrigin, argOther._fieldOrigin)
			&& _fieldTransform.CheckEqual(argOther._fieldTransform)
			&& _fieldMaterial.CheckEqual(argOther._fieldMaterial)
			&& _fieldObjectRay.CheckEqual(argOther._fieldObjectRay)
			&& _fieldCastsShadow == argOther._fieldCastsShadow;
	}
};

public class SphereGlass : Sphere {
	public SphereGlass() {
		this._fieldMaterial._fieldTransparency = 1.0;
		this._fieldMaterial._fieldRefractiveIndex = 1.5;
	}
	public SphereGlass(SphereGlass argOther) {
		this._fieldMaterial = argOther._fieldMaterial;
	}
};

public class Plane : Form {
	private ProjectMeta _fieldPM = new ProjectMeta();
	public override Vector GetNormalLocal(Point argPoint)
	{
		return new Vector(0, 1, 0);
	}
	public override Intersections GetIntersectionsLocal(Ray argRay)
	{
		if (Math.Abs(argRay._fieldDirection._fieldY) <= _fieldPM.getEpsilon())
		{
			return new Intersections();
		}
		double varTime = -argRay._fieldOrigin._fieldY / argRay._fieldDirection._fieldY;
		return new Intersections(varTime, this);
	}
};

public class AABBox : Form {
	private ProjectMeta _fieldPM = new ProjectMeta();
	public override Vector GetNormalLocal(Point argPoint)
	{
		double varMax = Math.Max(Math.Abs(argPoint._fieldX), Math.Max(Math.Abs(argPoint._fieldY),Math.Abs(argPoint._fieldZ)));
		if (varMax == Math.Abs(argPoint._fieldX)) {
			return new Vector(argPoint._fieldX, 0,0);
		} else if (varMax == Math.Abs(argPoint._fieldY)) {
			return new Vector(0,argPoint._fieldY, 0);
		}
		return new Vector(0,0,argPoint._fieldZ);
	}
	public override Intersections GetIntersectionsLocal(Ray argRay) {
		Tuple<double, double> varXMinToMax = GetAxisMinToMax(argRay._fieldOrigin._fieldX, argRay._fieldDirection._fieldX);
		Tuple<double, double> varYMinToMax = GetAxisMinToMax(argRay._fieldOrigin._fieldY, argRay._fieldDirection._fieldY);
		Tuple<double, double> varZMinToMax = GetAxisMinToMax(argRay._fieldOrigin._fieldZ, argRay._fieldDirection._fieldZ);
		double varMin = Math.Max(Math.Max(varXMinToMax.Item1, varYMinToMax.Item1), varZMinToMax.Item1);
		double varMax = Math.Min(Math.Min(varXMinToMax.Item2, varYMinToMax.Item2), varZMinToMax.Item2);
		Intersections varXs = new Intersections();
		if (varMin > varMax) {return varXs; }
		varXs.SetIntersect(varMin, this);
		varXs.SetIntersect(varMax, this);
		return varXs;
	}
	public Tuple<double, double> GetAxisMinToMax (double argOrigin, double argDirection) {
		double varMinNumerator = (-1-argOrigin);
		double varMaxNumerator = (1-argOrigin);
		double varMin = 0;
		double varMax = 0;
		if (Math.Abs(argDirection) >= _fieldPM.getEpsilon()) {
			varMin = varMinNumerator / argDirection;
			varMax = varMaxNumerator / argDirection;
		} else {
			varMin = varMinNumerator * double.PositiveInfinity;
			varMax = varMaxNumerator * double.PositiveInfinity;
		}
		if (varMin > varMax) { 
			double varTmp = varMin;
			varMin = varMax;
			varMax = varTmp;
		}
		return new Tuple<double, double> (varMin, varMax);
	}
}