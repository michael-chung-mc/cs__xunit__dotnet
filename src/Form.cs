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
	public bool _fieldClosed;
	public SpaceTuple _fieldOrigin;
	public double _fieldRadius;
	public double _fieldHeightMax;
	public double _fieldHeightMin;
	public Matrix _fieldTransform;
	public Matrix _fieldTransformInverse;
	public Matrix _fieldTransformInverseTranspose;
	public Material _fieldMaterial;
	public Ray _fieldObjectRay;
	public SpaceTuple _fieldVertexOne;
	public SpaceTuple _fieldVertexTwo;
	public SpaceTuple _fieldVertexThree;
	public SpaceTuple _fieldEdgeOneTwo;
	public SpaceTuple _fieldEdgeOneThree;
	public SpaceTuple _fieldNormal;
	public Form? _fieldParent;
	public List<Form> _fieldForms;
	public Form()
	{
		_fieldCastsShadow = true;
		_fieldClosed = false;
		_fieldOrigin = new Point(0, 0, 0);
		_fieldRadius = 0;
		_fieldHeightMax = 0;
		_fieldHeightMin = 0;
		SetTransform(new IdentityMatrix(4, 4));
		_fieldMaterial = new Material();
		_fieldObjectRay = new Ray();
		_fieldParent = null;
		_fieldForms = new List<Form>();
	}
	public Form(Form argOther)
	{
		_fieldCastsShadow = argOther._fieldCastsShadow;
		_fieldClosed = argOther._fieldClosed;
		_fieldOrigin = argOther._fieldOrigin;
		_fieldRadius = argOther._fieldRadius;
		_fieldHeightMax = argOther._fieldHeightMax;
		_fieldHeightMin = argOther._fieldHeightMin;
		SetTransform(argOther._fieldTransform);
		SetMaterial(argOther._fieldMaterial);
		_fieldObjectRay = argOther._fieldObjectRay;
		_fieldParent = argOther._fieldParent;
		_fieldForms = argOther._fieldForms;
	}
	public Intersections GetIntersections(Ray argRay)
	{
		this._fieldObjectRay = argRay.GetTransformed(this._fieldTransformInverse);
		return GetIntersectionsLocal(this._fieldObjectRay);
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
	public SpaceTuple GetNormal(SpaceTuple argPoint)
	{
		// Point varObjectPoint = _fieldTransformInverse * argPoint;
		// Vector varObjectNormal = GetNormalLocal(varObjectPoint);
		// Matrix varTransform = _fieldTransformInverse.GetTranspose();
		// Vector varWorldNormal = varTransform * varObjectNormal;
		// varWorldNormal._fieldW = 0;
		// return varWorldNormal.GetNormal();
		SpaceTuple varObjectPoint = GetObjectPointFromWorldSpace(argPoint);
		SpaceTuple varObjectNormal = GetNormalLocal(varObjectPoint);
		return GetWorldNormalFromObjectSpace(varObjectNormal);
	}
	public virtual SpaceTuple GetNormalLocal(SpaceTuple argPoint)
	{
		return new Vector(argPoint._fieldX,argPoint._fieldY,argPoint._fieldZ);
	}
	public Color GetColor(PointSource argLighting, SpaceTuple argWorldPosition, SpaceTuple argEye, SpaceTuple argNormal, bool argInShadow)
	{
		// Point varObjP = (this._fieldTransformInverse) * argPosition;
		// Point varPatternP = (this._fieldMaterial._fieldPattern._fieldTransformInverse) * varObjP;
		// Color varRes = this._fieldMaterial.GetColor(argLighting, varPatternP, argEye, argNormal, argInShadow);
		Color varRes = _fieldMaterial.GetColor(this, argLighting, argWorldPosition, argEye, argNormal, argInShadow);
		// Point varTransformedPoint = GetObjectPointFromWorldSpace(argWorldPosition);
		// Color varRes = _fieldMaterial.GetColor(this._fieldTransformInverse, argLighting, argWorldPosition, argEye, argNormal, argInShadow);
		return varRes;
	}
	// public Color GetColorShaded(PointSource argLighting, Point argPosition, Vector argEye, Vector argNormal, bool argInShadow)
	// {
	// 	Color varRes = _fieldMaterial.GetColor(argLighting, argPosition, argEye, argNormal, argInShadow);
	// 	return varRes;
	// }
	public Color GetColorLocal(Point argPosition)
	{
		// Point varObjectP = this._fieldTransformInverse * argPosition;
		// Point varPatternP = this._fieldMaterial._fieldPattern._fieldTransformInverse * varObjP;
		return this._fieldMaterial._fieldPattern.GetColor(this, argPosition);
	}
	public SpaceTuple GetObjectPointFromWorldSpace(SpaceTuple argPosition) {
		if (_fieldParent != null) { argPosition = new SpaceTuple(_fieldParent.GetObjectPointFromWorldSpace(argPosition)); }
		return _fieldTransformInverse * argPosition;
	}
	public SpaceTuple GetWorldNormalFromObjectSpace(SpaceTuple argNormal) {
		SpaceTuple varNormal = _fieldTransformInverseTranspose * argNormal;
		varNormal._fieldW = 0;
		varNormal = varNormal.GetNormal();
		if (_fieldParent != null) {
			varNormal = _fieldParent.GetWorldNormalFromObjectSpace(varNormal);
		}
		return varNormal;
	}
	public virtual AABB GetBounds() {
		return new AABB();
	}
	public void SetTransform(Matrix argMatrix) {
		this._fieldTransform = argMatrix;
		this._fieldTransformInverse = argMatrix.GetInverse();
		this._fieldTransformInverseTranspose = this._fieldTransformInverse.GetTranspose();
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

public class UnitSphere : Form {
	public UnitSphere()
	{
		_fieldRadius = 1.0;
	}
	public override Intersections GetIntersectionsLocal(Ray argRay)
	{
		Intersections varIntersections = new Intersections();
		SpaceTuple sphereToRay = argRay._fieldOrigin - new Point(0, 0, 0);
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
	public override SpaceTuple GetNormalLocal(SpaceTuple argPoint)
	{
		SpaceTuple varObjectNormal = argPoint - _fieldOrigin;
		return varObjectNormal.GetNormal();
	}
	public virtual AABB GetBounds() {
		return new AABB(new Point(-1,-1,-1), new Point(1,1,1));
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

public class UnitSphereGlass : UnitSphere {
	public UnitSphereGlass() {
		this._fieldMaterial._fieldTransparency = 1.0;
		this._fieldMaterial._fieldRefractiveIndex = 1.5;
	}
	public UnitSphereGlass(UnitSphereGlass argOther) {
		this._fieldMaterial = argOther._fieldMaterial;
	}
};

public class UnitPlane : Form {
	private ProjectMeta _fieldPM = new ProjectMeta();
	public override SpaceTuple GetNormalLocal(SpaceTuple argPoint)
	{
		return new Vector(0, 1, 0);
	}
	public override Intersections GetIntersectionsLocal(Ray argRay)
	{
		if (Math.Abs(argRay._fieldDirection._fieldY) <= _fieldPM.GetEpsilon())
		{
			return new Intersections();
		}
		double varTime = -argRay._fieldOrigin._fieldY / argRay._fieldDirection._fieldY;
		return new Intersections(varTime, this);
	}
};

public class UnitCube : Form {
	private ProjectMeta _fieldPM = new ProjectMeta();
	public override SpaceTuple GetNormalLocal(SpaceTuple argPoint)
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
		if (Math.Abs(argDirection) >= _fieldPM.GetEpsilon()) {
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

public class UnitCylinder : Form {
	private ProjectMeta _fieldPM = new ProjectMeta();
	public UnitCylinder() {
		_fieldHeightMax = double.MaxValue;
		_fieldHeightMin = double.MinValue;
	}
	public override SpaceTuple GetNormalLocal(SpaceTuple argPoint)
	{
		double varDistance = argPoint._fieldX * argPoint._fieldX + argPoint._fieldZ * argPoint._fieldZ;
		if (varDistance < 1 && argPoint._fieldY >= _fieldHeightMax - _fieldPM.GetEpsilon()) { return new Vector(0,1,0); }
		else if (varDistance < 1 && argPoint._fieldY <= _fieldHeightMin + _fieldPM.GetEpsilon()) { return new Vector(0,-1,0); }
		return new Vector(argPoint._fieldX, 0, argPoint._fieldZ);
	}
	public override Intersections GetIntersectionsLocal(Ray argRay) {
		Intersections varXs = new Intersections();
		double varA = argRay._fieldDirection._fieldX * argRay._fieldDirection._fieldX + argRay._fieldDirection._fieldZ * argRay._fieldDirection._fieldZ;
		if (Math.Abs(varA) > _fieldPM.GetEpsilon()) {
			double varB = (2 * argRay._fieldOrigin._fieldX * argRay._fieldDirection._fieldX) + (2 * argRay._fieldOrigin._fieldZ * argRay._fieldDirection._fieldZ);
			double varC = (argRay._fieldOrigin._fieldX * argRay._fieldOrigin._fieldX) + (argRay._fieldOrigin._fieldZ * argRay._fieldOrigin._fieldZ) - 1;
			double varDiscriminant = (varB*varB) - (4 * varA * varC);
			if (varDiscriminant < 0) { return varXs; }
			double varTimeZero = (-varB - Math.Sqrt(varDiscriminant)) / (2*varA);
			double varTimeOne = (-varB + Math.Sqrt(varDiscriminant)) / (2*varA);
			if (varTimeZero > varTimeOne) {
				double varTmp = varTimeZero;
				varTimeZero = varTimeOne;
				varTimeOne = varTmp;
			}
			double varIntersectYZero = argRay._fieldOrigin._fieldY + varTimeZero * argRay._fieldDirection._fieldY;
			if (_fieldHeightMin < varIntersectYZero && varIntersectYZero < _fieldHeightMax) {varXs.SetIntersect(varTimeZero, this); }
			double varIntersectYOne = argRay._fieldOrigin._fieldY + varTimeOne * argRay._fieldDirection._fieldY;
			if (_fieldHeightMin < varIntersectYOne && varIntersectYOne < _fieldHeightMax) {varXs.SetIntersect(varTimeOne, this); }

		}
		SetIntersectionsCaps(argRay, ref varXs);
		return varXs;
	}
	public bool CheckCaps(Ray argRay, double time) {
		double varX = argRay._fieldOrigin._fieldX + time * argRay._fieldDirection._fieldX;
		double varZ = argRay._fieldOrigin._fieldZ + time * argRay._fieldDirection._fieldZ;
		return (varX*varX + varZ*varZ) <= 1.0;
	}
	public void SetIntersectionsCaps (Ray argRay, ref Intersections argXs) {
		if (_fieldClosed && Math.Abs(argRay._fieldDirection._fieldY) > _fieldPM.GetEpsilon()) {
			double varTime = (_fieldHeightMin - argRay._fieldOrigin._fieldY) / argRay._fieldDirection._fieldY;
			if (CheckCaps(argRay, varTime)) {argXs.SetIntersect(varTime, this);}
			varTime = (_fieldHeightMax - argRay._fieldOrigin._fieldY) / argRay._fieldDirection._fieldY;
			if (CheckCaps(argRay, varTime)) {argXs.SetIntersect(varTime, this);}
		}
	}
}

public class UnitDNCone : Form {
	private ProjectMeta _fieldPM = new ProjectMeta();
	public UnitDNCone() {
		_fieldHeightMax = double.MaxValue;
		_fieldHeightMin = double.MinValue;
	}
	public virtual SpaceTuple GetNormalLocal(SpaceTuple argPoint)
	{
		double varY = Math.Sqrt(argPoint._fieldX * argPoint._fieldX + argPoint._fieldZ * argPoint._fieldZ);
		varY = argPoint._fieldY > 0 ? -varY : varY;
		return new Vector(argPoint._fieldX, varY, argPoint._fieldZ);
	}
	public override Intersections GetIntersectionsLocal(Ray argRay) {
		double varUncertainty = _fieldPM.GetEpsilon();
		Intersections varXs = new Intersections();
		double varA = argRay._fieldDirection._fieldX * argRay._fieldDirection._fieldX - argRay._fieldDirection._fieldY * argRay._fieldDirection._fieldY + argRay._fieldDirection._fieldZ * argRay._fieldDirection._fieldZ;
		double varB = (2.0 * argRay._fieldOrigin._fieldX * argRay._fieldDirection._fieldX) - (2.0 * argRay._fieldOrigin._fieldY * argRay._fieldDirection._fieldY) + (2.0 * argRay._fieldOrigin._fieldZ * argRay._fieldDirection._fieldZ);
		if (Math.Abs(varA) < varUncertainty && Math.Abs(varB) < varUncertainty) { return varXs; }
		else if (Math.Abs(varA) <= varUncertainty && Math.Abs(varB) > varUncertainty) {
			double varC = (argRay._fieldOrigin._fieldX * argRay._fieldOrigin._fieldX) - (argRay._fieldOrigin._fieldY * argRay._fieldOrigin._fieldY) + (argRay._fieldOrigin._fieldZ * argRay._fieldOrigin._fieldZ);
			double varIntersectionTime = -varC/(2*varB);
			varXs.SetIntersect(varIntersectionTime, this);
		}
		else if (Math.Abs(varA) > varUncertainty) {
			double varC = (argRay._fieldOrigin._fieldX * argRay._fieldOrigin._fieldX) - (argRay._fieldOrigin._fieldY * argRay._fieldOrigin._fieldY) + (argRay._fieldOrigin._fieldZ * argRay._fieldOrigin._fieldZ);
			double varDiscriminant = (varB*varB) - (4 * varA * varC);
			varUncertainty *= Math.Max(Math.Max(Math.Abs(varA), Math.Abs(varB)), Math.Abs(varC));
			if (varDiscriminant >=  -varUncertainty) {
				double varTimeZero = (-varB - Math.Sqrt(varDiscriminant)) / (2*varA);
				double varTimeOne = (-varB + Math.Sqrt(varDiscriminant)) / (2*varA);
				if (varTimeZero > varTimeOne) {
					double varTmp = varTimeZero;
					varTimeZero = varTimeOne;
					varTimeOne = varTmp;
				}
				double varIntersectYZero = argRay._fieldOrigin._fieldY + varTimeZero * argRay._fieldDirection._fieldY;
				if (_fieldHeightMin < varIntersectYZero && varIntersectYZero < _fieldHeightMax) {varXs.SetIntersect(varTimeZero, this); }
				double varIntersectYOne = argRay._fieldOrigin._fieldY + varTimeOne * argRay._fieldDirection._fieldY;
				if (_fieldHeightMin < varIntersectYOne && varIntersectYOne < _fieldHeightMax) {varXs.SetIntersect(varTimeOne, this); }
			}
		}
		SetIntersectionsCaps(argRay, ref varXs);
		return varXs;
	}
	public bool CheckCaps(Ray argRay, double argHeight, double time) {
		double varX = argRay._fieldOrigin._fieldX + time * argRay._fieldDirection._fieldX;
		double varZ = argRay._fieldOrigin._fieldZ + time * argRay._fieldDirection._fieldZ;
		return (varX*varX + varZ*varZ) <= argHeight * argHeight;
	}
	public void SetIntersectionsCaps (Ray argRay, ref Intersections argXs) {
		if (_fieldClosed && Math.Abs(argRay._fieldDirection._fieldY) > _fieldPM.GetEpsilon()) {
			double varTime = (_fieldHeightMin - argRay._fieldOrigin._fieldY) / argRay._fieldDirection._fieldY;
			if (CheckCaps(argRay, _fieldHeightMax, varTime)) {argXs.SetIntersect(varTime, this);}
			varTime = (_fieldHeightMax - argRay._fieldOrigin._fieldY) / argRay._fieldDirection._fieldY;
			if (CheckCaps(argRay, _fieldHeightMin, varTime)) {argXs.SetIntersect(varTime, this);}
		}
	}
}

public class CompositeGroup : Form {
	public override SpaceTuple GetNormalLocal(SpaceTuple argPoint)
	{
		throw new InvalidOperationException("Groups Don't Have Normals");
	}
	public void SetObject(CompositeGroup argObject) {
		argObject._fieldParent = this;
		_fieldForms.Add(argObject);
	}
	public void SetObject(Form argObject) {
		argObject._fieldParent = this;
		_fieldForms.Add(argObject);
	}
	public override Intersections GetIntersectionsLocal(Ray argRay) {
		Intersections varXs = new Intersections();
		for ( int i = 0; i < _fieldForms.Count; ++i ) {
			List<Intersection> varLocalXs = _fieldForms[i].GetIntersections(argRay)._fieldIntersections;
			for (int j=0; j < varLocalXs.Count; ++j) {
				varXs.SetIntersect(varLocalXs[j]._fieldTime, varLocalXs[j]._fieldObject);
			}
		}
		return varXs;
	}
}

public class UnitTriangle : Form {
	private ProjectMeta _fieldPM = new ProjectMeta();
	public UnitTriangle(Point argVertexOne, Point argVertexTwo, Point argVertexThree) {
		_fieldVertexOne = argVertexOne;
		_fieldVertexTwo = argVertexTwo;
		_fieldVertexThree = argVertexThree;
		_fieldEdgeOneTwo = _fieldVertexTwo - _fieldVertexOne;
		_fieldEdgeOneThree = _fieldVertexThree - _fieldVertexOne;
		_fieldNormal = (_fieldEdgeOneThree.GetCrossProduct(_fieldEdgeOneTwo)).GetNormal();
	}
	public override SpaceTuple GetNormalLocal(SpaceTuple argPoint)
	{
		return _fieldNormal;
	}
	public override Intersections GetIntersectionsLocal(Ray argRay) {
		Intersections varXs = new Intersections();
		SpaceTuple varDirCrossEdge = argRay._fieldDirection.GetCrossProduct(_fieldEdgeOneThree);
		double varDeterminant = _fieldEdgeOneTwo.GetDotProduct(varDirCrossEdge);
		double varUncertainty = _fieldPM.GetEpsilon();
		if (Math.Abs(varDeterminant) <= varUncertainty) {
			return varXs;
		}
		double varF = 1.0 / varDeterminant;
		SpaceTuple varPointToOrigin = argRay._fieldOrigin - _fieldVertexOne;
		double varU = varF * varPointToOrigin.GetDotProduct(varDirCrossEdge);
		if (varU < 0 || varU > 1) {
			return varXs;
		}
		SpaceTuple varOriginCrossEdgeOne = varPointToOrigin.GetCrossProduct(_fieldEdgeOneTwo);
		double varV = varF * argRay._fieldDirection.GetDotProduct(varOriginCrossEdgeOne);
		if (varV < 0 || (varU + varV) > 1) {
			return varXs;
		}
		double varTime = varF * _fieldEdgeOneThree.GetDotProduct(varOriginCrossEdgeOne);
		varXs.SetIntersect(varTime, this);
		return varXs;
	}
}

public class AABB : Form {
	public SpaceTuple _fieldMin;
	public SpaceTuple _fieldMax;
	public AABB () {
		_fieldMin = new Point(double.MaxValue, double.MaxValue, double.MaxValue);
		_fieldMax = new Point(double.MinValue, double.MinValue, double.MinValue);
	}
	public AABB (SpaceTuple argMin, SpaceTuple argMax) {
		_fieldMin = argMin;
		_fieldMax = argMax;
	}
	public void SetPoint(Point argPoint) {
		double varMinX = Math.Min(_fieldMin._fieldX,argPoint._fieldX);
		double varMinY = Math.Min(_fieldMin._fieldY,argPoint._fieldY);
		double varMinZ = Math.Min(_fieldMin._fieldZ,argPoint._fieldZ);
		_fieldMin.SetPoints(varMinX, varMinY, varMinZ, 1);
		double varMaxX = Math.Max(_fieldMax._fieldX,argPoint._fieldX);
		double varMaxY = Math.Max(_fieldMax._fieldY,argPoint._fieldY);
		double varMaxZ = Math.Max(_fieldMax._fieldZ,argPoint._fieldZ);
		_fieldMax.SetPoints(varMaxX, varMaxY, varMaxZ, 1);
	}
}