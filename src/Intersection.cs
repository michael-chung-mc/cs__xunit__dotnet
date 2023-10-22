using LibForm;
using LibTuple;
using LibRay;
using LibProjectMeta;
namespace LibIntersection;

public class IntersectionState {
	public double _fieldTime;
	public Form _fieldObject;
	public SpaceTuple _fieldPoint;
	public SpaceTuple _fieldOverPoint;
	public SpaceTuple _fieldUnderPoint;
	public SpaceTuple _fieldPov;
	public SpaceTuple _fieldNormal;
	public SpaceTuple _fieldReflect;
	public bool _fieldInside;
	public double _fieldRefractiveIndexOne;
	public double _fieldRefractiveIndexTwo;
	public IntersectionState(){
		_fieldTime = 0;
		_fieldObject = new Form();
		_fieldPoint = new Point();
		_fieldPov = new Vector();
		_fieldNormal = new Vector();
		_fieldInside = false;
		_fieldUnderPoint = new Point();
		_fieldReflect = new Vector();
		_fieldOverPoint = new Point();
		_fieldRefractiveIndexOne = 0.0;
		_fieldRefractiveIndexTwo = 0.0;
	}
	public IntersectionState(IntersectionState argOther) {
		_fieldTime = argOther._fieldTime;
		SetObject(argOther._fieldObject);
		_fieldPoint = argOther._fieldPoint;
		_fieldPov = argOther._fieldPov;
		_fieldNormal = argOther._fieldNormal;
		_fieldInside = argOther._fieldInside;
		_fieldOverPoint = argOther._fieldOverPoint;
		_fieldReflect = argOther._fieldReflect;
		_fieldUnderPoint = argOther._fieldUnderPoint;
		_fieldRefractiveIndexOne = argOther._fieldRefractiveIndexOne;
		_fieldRefractiveIndexTwo = argOther._fieldRefractiveIndexTwo;
	}
	public double GetSchlick() {
		double varEyeNormal = this._fieldPov.GetDotProduct(this._fieldNormal);
		if (this._fieldRefractiveIndexOne > this._fieldRefractiveIndexTwo) {
			double varNToN = this._fieldRefractiveIndexOne/this._fieldRefractiveIndexTwo;
			double varSinTSqr = (varNToN*varNToN) * (1.0-(varEyeNormal*varEyeNormal));
			if (varSinTSqr > 1.0) { return 1.0;}
			varEyeNormal = Math.Sqrt(1.0-varSinTSqr);
		}
		double varR0 = Math.Pow((this._fieldRefractiveIndexOne - this._fieldRefractiveIndexTwo)/(this._fieldRefractiveIndexOne+this._fieldRefractiveIndexTwo),2);
		return varR0 + (1-varR0) * Math.Pow((1-varEyeNormal),5);
	}
    public void SetObject(Form argObject) {
		_fieldObject = argObject;
	}
	public void RenderConsole() {
		Console.WriteLine("IntersectionState::renderConsole()");
		Console.WriteLine($"IntersectionState::mbrTime: {_fieldTime}");
		Console.WriteLine("IntersectionState::mbrObject: ");
		_fieldObject.RenderConsole();
		Console.WriteLine("IntersectionState::mbrPoint: ");
		_fieldPoint.RenderConsole();
		Console.WriteLine("IntersectionState::mbrEye: ");
		_fieldPov.RenderConsole();
		Console.WriteLine("IntersectionState::mbrNormal: ");
		_fieldNormal.RenderConsole();
		Console.WriteLine($"IntersectionState::mbrInside: {_fieldInside}");
		Console.WriteLine("IntersectionState::mbrOverPoint: ");
		_fieldOverPoint.RenderConsole();
		Console.WriteLine("IntersectionState::mbrReflect: ");
		_fieldReflect.RenderConsole();
	}
};

public class Intersection {
	public bool _fieldExists;
	public double _fieldTime;
	public double _fieldU;
	public double _fieldV;
	public Form _fieldObject;
	protected ProjectMeta varPM = new ProjectMeta();
	public Intersection()
	{
		_fieldObject = new Form();
		_fieldTime = 0;
		_fieldExists = true;
	}
	public Intersection(Intersection argOther){
		SetObject(argOther._fieldObject);
		_fieldTime = argOther._fieldTime;
		_fieldExists = argOther._fieldExists;
	}
	public Intersection(double argTime, Form argObj)
	{
		SetObject(argObj);
		_fieldTime = argTime;
		_fieldExists = true;
	}
	public Intersection(double argTime, Form argObj, double argU, double argV)
	{
		SetObject(argObj);
		_fieldTime = argTime;
		_fieldExists = true;
		_fieldU = argU;
		_fieldV = argV;
	}
	public bool CheckEqual(Intersection argOther)
	{
		return _fieldTime == argOther._fieldTime && _fieldObject.CheckEqual(argOther._fieldObject) && _fieldExists == argOther._fieldExists;
	}
	public IntersectionState GetState(Ray argRay, List<Intersection> argIntersections)
	{
		IntersectionState varIs = new IntersectionState();
		varIs._fieldTime = _fieldTime;
		varIs.SetObject(_fieldObject);
		Intersection varHit = new Intersection(varIs._fieldTime, varIs._fieldObject, _fieldU, _fieldV);
		varIs._fieldPoint = argRay.GetPosition(varIs._fieldTime);
		varIs._fieldPov = -argRay._fieldDirection;
		varIs._fieldNormal = varIs._fieldObject.GetNormal(varIs._fieldPoint, varHit);
		if (varIs._fieldNormal.GetDotProduct(varIs._fieldPov) < 0) {
			varIs._fieldInside = true;
			varIs._fieldNormal = -varIs._fieldNormal;
		}
		else {
			varIs._fieldInside = false;
		}
		varIs._fieldOverPoint = varIs._fieldPoint + (varIs._fieldNormal * varPM.GetEpsilon());
		varIs._fieldUnderPoint = varIs._fieldPoint - (varIs._fieldNormal * varPM.GetEpsilon());
		varIs._fieldReflect = argRay._fieldDirection.GetReflect(varIs._fieldNormal);
		List<Form> varHitObjects = new List<Form>();
		for (int i = 0; i < argIntersections.Count; ++i) {
			if (varHit.CheckEqual(argIntersections[i])) {
				if (varHitObjects.Count == 0) { varIs._fieldRefractiveIndexOne = 1.0; }
				else { varIs._fieldRefractiveIndexOne = varHitObjects[varHitObjects.Count-1]._fieldMaterial._fieldRefractiveIndex; }
			}
			int varFound = -1;
			for (int j = 0; j < varHitObjects.Count; ++j)
			{
				if (varHitObjects[j].CheckEqual(argIntersections[i]._fieldObject)) {
					varFound = j;
					break;
				}
			}
			if (varFound < 0) { varHitObjects.Add(argIntersections[i]._fieldObject); }
			else { varHitObjects.RemoveAt(varFound); }
			if (varHit.CheckEqual(argIntersections[i])) {
				if (varHitObjects.Count == 0) { varIs._fieldRefractiveIndexTwo = 1.0; }
				else { varIs._fieldRefractiveIndexTwo = varHitObjects[varHitObjects.Count-1]._fieldMaterial._fieldRefractiveIndex; }
				break;
			}
		}
		return varIs;
	}
	public void SetObject(Form argObject) {
		_fieldObject = argObject;
	}
}

public class Intersections {
    public List<Intersection> _fieldIntersections;
	public Intersections()
	{
		_fieldIntersections = new List<Intersection>();
	}
	public Intersections(Intersections argOther)
	{
		_fieldIntersections = new List<Intersection>();
		for (int i = 0; i < argOther._fieldIntersections.Count(); ++i)
		{
			SetIntersect(argOther._fieldIntersections[i]);
		}
	}
	public Intersections(double argTime, Form argObj)
	{
		_fieldIntersections = new List<Intersection>();
		SetIntersect(argTime, argObj);
	}
	public Intersections(double argTime, Form argObj, double argU, double argV)
	{
		_fieldIntersections = new List<Intersection>();
		SetIntersect(argTime, argObj, argU, argV);
	}
	public Intersection GetHit() {
		int index = -1;
		for (int i = 0; i < _fieldIntersections.Count(); ++i)
		{
			if ((_fieldIntersections[i]._fieldTime > 0)
				&& ((index < 0) || (_fieldIntersections[i]._fieldTime <= _fieldIntersections[index]._fieldTime)))
			{
				index = i;
				break;
			}
		}
		if (index < 0) {
			Intersection varIx = new Intersection();
			varIx._fieldExists = false;
			return varIx;
		}
		else {
			return _fieldIntersections[index];
		}
	}
	public void SetIntersect(double argTime, Form argObject)
	{
		_fieldIntersections.Add(new Intersection(argTime, argObject));
		SortIntersections();
	}
	public void SetIntersect(double argTime, Form argObject, double argU, double argV)
	{
		_fieldIntersections.Add(new Intersection(argTime, argObject, argU, argV));
		SortIntersections();
	}
	public void SetIntersect(Intersection argIx) {
		_fieldIntersections.Add(argIx);
		SortIntersections();
	}
	private void SortIntersections () {
		_fieldIntersections.Sort(delegate(Intersection argA, Intersection argB) {
			return argA._fieldTime.CompareTo(argB._fieldTime);
		});
	}
};