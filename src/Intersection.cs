using LibForm;
using LibTuple;
using LibRay;
using LibProjectMeta;
namespace LibIntersection;

public class IntersectionState {
	public double _fieldTime;
	public Form _fieldObject;
	public Point _fieldPoint;
	public Point _fieldOverPoint;
	public Point _fieldUnderPoint;
	public Vector _fieldPov;
	public Vector _fieldNormal;
	public Vector _fieldReflect;
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
		_fieldOverPoint = argOther._fieldOverPoint;
		_fieldRefractiveIndexOne = argOther._fieldRefractiveIndexOne;
		_fieldRefractiveIndexTwo = argOther._fieldRefractiveIndexTwo;
	}
	// IntersectionState::IntersectionState( IntersectionState &&argOther) noexcept {
	// 	mbrTime = argOther._fieldTime;
	// 	mbrObject = std::move(argOther._fieldObject);
	// 	argOther._fieldObject = nullptr;
	// 	mbrPoint = argOther._fieldPoint;
	// 	mbrEye = argOther._fieldEye;
	// 	mbrNormal = argOther._fieldNormal;
	// 	mbrInside = argOther._fieldInside;
	// 	mbrOverPoint = argOther._fieldOverPoint;
	// 	mbrReflect = argOther._fieldReflect;
	// 	mbrOverPoint = argOther._fieldOverPoint;
	// 	mbrRefractiveIndexOne = argOther._fieldRefractiveIndexOne;
	// 	mbrRefractiveIndexTwo = argOther._fieldRefractiveIndexTwo;
	// }
	// IntersectionState& IntersectionState::operator=(IntersectionState &argOther) {
	// 	if (this == &argOther) return *this;
	// 	mbrTime = argOther._fieldTime;
	// 	setObject(argOther._fieldObject);
	// 	mbrPoint = argOther._fieldPoint;
	// 	mbrEye = argOther._fieldEye;
	// 	mbrNormal = argOther._fieldNormal;
	// 	mbrInside = argOther._fieldInside;
	// 	mbrOverPoint = argOther._fieldOverPoint;
	// 	mbrReflect = argOther._fieldReflect;
	// 	mbrOverPoint = argOther._fieldOverPoint;
	// 	mbrRefractiveIndexOne = argOther._fieldRefractiveIndexOne;
	// 	mbrRefractiveIndexTwo = argOther._fieldRefractiveIndexTwo;
	// 	return *this;
	// }
	// IntersectionState& IntersectionState::operator=(IntersectionState &&argOther) noexcept {
	// 	if (this == &argOther) return *this;
	// 	mbrTime = argOther._fieldTime;
	// 	mbrObject = std::move(argOther._fieldObject);
	// 	argOther._fieldObject = nullptr;
	// 	mbrPoint = argOther._fieldPoint;
	// 	mbrEye = argOther._fieldEye;
	// 	mbrNormal = argOther._fieldNormal;
	// 	mbrInside = argOther._fieldInside;
	// 	mbrOverPoint = argOther._fieldOverPoint;
	// 	mbrReflect = argOther._fieldReflect;
	// 	mbrOverPoint = argOther._fieldOverPoint;
	// 	mbrRefractiveIndexOne = argOther._fieldRefractiveIndexOne;
	// 	mbrRefractiveIndexTwo = argOther._fieldRefractiveIndexTwo;
	// 	return *this;
	// }
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
	public Form _fieldObject;
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
	// public Intersection(Intersection &&argOther) noexcept;
	// public Intersection(double time, Form *s)
	// {
	// 	setObject(s);
	// 	mbrTime = time;
	// 	mbrExists = true;
	// }
	public Intersection(double argTime, Form argObj)
	{
		SetObject(argObj);
		_fieldTime = argTime;
		_fieldExists = true;
	}
	// Intersection(double time, Form> s);
	// public virtual ~Intersection() = default;
	// public Intersection& operator=(Intersection &argOther);
	// public Intersection& operator=(Intersection &&argOther) noexcept;
	// Intersection::Intersection(Intersection &&argOther) noexcept {
	// 	mbrObject = std::move(argOther._fieldObject);
	// 	argOther._fieldObject = nullptr;
	// 	mbrTime = argOther._fieldTime;
	// 	mbrExists = argOther._fieldExists;
	// }
	// Intersection& Intersection::operator=(Intersection &argOther){
	// 	if (this == &argOther) return *this;
	// 	setObject(argOther._fieldObject);
	// 	mbrTime = argOther._fieldTime;
	// 	mbrExists = argOther._fieldExists;
	// 	return *this;
	// }
	// Intersection& Intersection::operator=(Intersection &&argOther) noexcept {
	// 	if (this == &argOther) return *this;
	// 	mbrObject = std::move(argOther._fieldObject);
	// 	argOther._fieldObject = nullptr;
	// 	mbrTime = argOther._fieldTime;
	// 	mbrExists = argOther._fieldExists;
	// 	return *this;
	// }
	public bool CheckEqual(Intersection argOther)
	{
		return _fieldTime == argOther._fieldTime && _fieldObject.CheckEqual(argOther._fieldObject);
	}

	// IntersectionState Intersection::getState(Ray argRay)
	// {
	// 	IntersectionState is = IntersectionState();
	// 	is._fieldTime = mbrTime;
	// 	is.setObject(mbrObject);
	// 	is._fieldPoint = argRay.getPosition(is._fieldTime);
	// 	is._fieldEye = -(argRay._fieldDirection);
	// 	is._fieldNormal = is._fieldObject.getNormal(is._fieldPoint);
	// 	if (is._fieldNormal.dot(is._fieldEye) < 0) {
	// 		is._fieldInside = true;
	// 		is._fieldNormal = -is._fieldNormal;
	// 	}
	// 	else {
	// 		is._fieldInside = false;
	// 	}
	// 	is._fieldOverPoint = is._fieldPoint + is._fieldNormal * getEpsilon();
	// 	is._fieldReflect = argRay._fieldDirection.reflect(is._fieldNormal);
	// 	return is;
	// }
	// IntersectionState Intersection::getState(Ray argRay, Intersections &argIntersections)
	// {
	// 	IntersectionState is = IntersectionState();
	// 	is._fieldTime = mbrTime;
	// 	is.setObject(mbrObject);
	// 	is._fieldPoint = argRay.getPosition(is._fieldTime);
	// 	is._fieldEye = -(argRay._fieldDirection);
	// 	is._fieldNormal = is._fieldObject.getNormal(is._fieldPoint);
	// 	if (is._fieldNormal.dot(is._fieldEye) < 0) {
	// 		is._fieldInside = true;
	// 		is._fieldNormal = -is._fieldNormal;
	// 	}
	// 	else {
	// 		is._fieldInside = false;
	// 	}
	// 	is._fieldOverPoint = is._fieldPoint + is._fieldNormal * getEpsilon();
	// 	is._fieldReflect = argRay._fieldDirection.reflect(is._fieldNormal);
	// 	List<Form*> varHitObjects;
	// 	// Intersection varHit = argIntersections.hit();
	// 	Intersection varHit = Intersection(is._fieldTime,mbrObject);
	// 	for (int i = 0; i < argIntersections._fieldIntersections.Count(); ++i) {
	// 		if (varHit.checkEqual(*argIntersections._fieldIntersections[i])) {
	// 			if (varHitObjects.Count() == 0) { is._fieldRefractiveIndexOne = 1.0; }
	// 			else { is._fieldRefractiveIndexOne = varHitObjects.back()._fieldMaterial._fieldRefractiveIndex; }
	// 		}
	// 		int varFound = -1;
	// 		List<Form*>::iterator varItr = varHitObjects.begin();
	// 		for (; varItr != varHitObjects.end(); ++varItr)
	// 		{
	// 			if (*varItr == varHit._fieldObject) {
	// 				varHitObjects.erase(varItr);
	// 				varFound = 1;
	// 				break;
	// 			}
	// 			// if (varHitObjects[j] == varHit._fieldObject)
	// 			// {
	// 			// 	varFound = j;
	// 			// 	break;
	// 			// }
	// 		}
	// 		// if (varFound > 0) {varHitObjects.erase(varHitObjects.begin()+varFound);}
	// 		// else {varHitObjects.Add(varHit._fieldObject);}
	// 		if (varFound < 0) {varHitObjects.Add(varHit._fieldObject);}
	// 		if (varHit.checkEqual(*argIntersections._fieldIntersections[i])) {
	// 			if (varHitObjects.empty()) { is._fieldRefractiveIndexTwo = 1.0; }
	// 			else { is._fieldRefractiveIndexTwo = varHitObjects.back()._fieldMaterial._fieldRefractiveIndex; }
	// 			break;
	// 		}
	// 	}
	// 	return is;
	// }
	public IntersectionState GetState(Ray argRay, List<Intersection> argIntersections)
	{
		ProjectMeta varPM = new ProjectMeta();
		IntersectionState varIs = new IntersectionState();
		varIs._fieldTime = _fieldTime;
		// Console.WriteLine($"Intersection::getState()::varIs._fieldTime=({varIs._fieldTime}");
		varIs.SetObject(_fieldObject);
		varIs._fieldPoint = argRay.GetPosition(varIs._fieldTime);
		// Console.WriteLine($"Intersection::getState()::varIs._fieldPoint=(x:{varIs._fieldPoint._fieldX},y:{varIs._fieldPoint._fieldY},z:{varIs._fieldPoint._fieldZ},w:{varIs._fieldPoint._fieldW}");
		varIs._fieldPov = -argRay._fieldDirection;
		varIs._fieldNormal = varIs._fieldObject.GetNormal(varIs._fieldPoint);
		if (varIs._fieldNormal.GetDotProduct(varIs._fieldPov) < 0) {
			varIs._fieldInside = true;
			varIs._fieldNormal = -varIs._fieldNormal;
		}
		else {
			varIs._fieldInside = false;
		}
		varIs._fieldOverPoint = varIs._fieldPoint + (varIs._fieldNormal * varPM.getEpsilon());
		// Console.WriteLine($"Intersection::getState()::varIs._fieldOverPoint=(x:{varIs._fieldOverPoint._fieldX},y:{varIs._fieldOverPoint._fieldY},z:{varIs._fieldOverPoint._fieldZ},w:{varIs._fieldOverPoint._fieldW}");
		// Console.WriteLine($" = Intersection::getState()::varIs._fieldNormal=(x:{varIs._fieldNormal._fieldX},y:{varIs._fieldNormal._fieldY},z:{varIs._fieldNormal._fieldZ},w:{varIs._fieldNormal._fieldW}");
		varIs._fieldReflect = argRay._fieldDirection.GetReflect(varIs._fieldNormal);
		List<Form> varHitObjects = new List<Form>();
		Intersection varHit = new Intersection(varIs._fieldTime, _fieldObject);
		// Console.WriteLine($"Intersection::getState()::hit._fieldTime({varHit._fieldTime}");
		for (int i = 0; i < argIntersections.Count(); ++i) {
			// Console.WriteLine($"Intersection::getState()::argIntersections[i]._fieldTime({argIntersections[i]._fieldTime}");
			// Console.WriteLine($"Intersection::getState()::argIntersections[i]._fieldObject._fieldMaterial._fieldRefractiveIndex({argIntersections[i]._fieldObject._fieldMaterial._fieldRefractiveIndex}");
			if (varHit.CheckEqual(argIntersections[i])) {
				if (varHitObjects.Count() == 0) { varIs._fieldRefractiveIndexOne = 1.0; }
				else { varIs._fieldRefractiveIndexOne = varHitObjects[varHitObjects.Count()-1]._fieldMaterial._fieldRefractiveIndex; }
			}
			int varFound = -1;
			for (int j = 0; j < varHitObjects.Count(); j++)
			{
				if (varHitObjects[j].CheckEqual(argIntersections[i]._fieldObject)) {
					// Console.WriteLine($"Intersection::getState()::erase(argIntersections[i]._fieldObject({argIntersections[i]._fieldObject._fieldMaterial._fieldRefractiveIndex}");
					varHitObjects.RemoveAt(j);
					varFound = 1;
					break;
				}
			}
			if (varFound < 0) {
				varHitObjects.Add(argIntersections[i]._fieldObject);
				// Console.WriteLine($"Intersection::getState()::varHitObjects.back()._fieldMaterial._fieldRefractiveIndex({varHitObjects[varHitObjects.Count()-1]._fieldMaterial._fieldRefractiveIndex}");
			}
			if (varHit.CheckEqual(argIntersections[i])) {
				if (varHitObjects.Count == 0) { varIs._fieldRefractiveIndexTwo = 1.0; }
				else { varIs._fieldRefractiveIndexTwo = varHitObjects[varHitObjects.Count-1]._fieldMaterial._fieldRefractiveIndex; }
				break;
			}
			// Console.WriteLine($"Intersection::getState()::varIs._fieldRefractiveIndexOne{varIs._fieldRefractiveIndexOne}");
			// Console.WriteLine($"Intersection::getState()::varIs._fieldRefractiveIndexTwo{varIs._fieldRefractiveIndexTwo}");
		}
		// Console.WriteLine($"Intersection::getState()::varIs._fieldRefractiveIndexOne{varIs._fieldRefractiveIndexOne}");
		// Console.WriteLine($"Intersection::getState()::varIs._fieldRefractiveIndexTwo{varIs._fieldRefractiveIndexTwo}");
		varIs._fieldUnderPoint = varIs._fieldPoint - (varIs._fieldNormal * varPM.getEpsilon());
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
		//Intersection i = Intersection(0, Sphere());
		//intersections.Add(i);
	}
	public Intersections(Intersections argOther)
	{
		_fieldIntersections = new List<Intersection>();
		for (int i = 0; i < argOther._fieldIntersections.Count(); ++i)
		{
			SetIntersection(argOther._fieldIntersections[i]);
			// Intersection varIs = other._fieldIntersections[i];
			// mbrIntersections.Add(varIs);
		}
		//mbrIntersections = other._fieldIntersections;
	}
	public Intersections(double argTime, Form argObj)
	{
		_fieldIntersections = new List<Intersection>();
		SetIntersection(argTime, argObj);
		// Intersection i = Intersection(t,std::make_unique<Form>(*s));
		// mbrIntersections.Add(i);
	}
	// virtual ~Intersections() = default;
	// Intersections& operator=(Intersections other)
	// {
	// 	if (this == &other) return *this;
	// 	mbrIntersections.clear();
	// 	for (int i = 0; i < other._fieldIntersections.Count(); ++i)
	// 	{
	// 		setIntersection(other._fieldIntersections[i]);
	// 		// Intersection varIs = other._fieldIntersections[i];
	// 		// mbrIntersections.Add(varIs);
	// 	}
	// 	// mbrIntersections = other._fieldIntersections;
	// 	return *this;
	// }
	// void Intersections::intersect(double t, Form> argObject)
	// {
	// 	auto funcComp = [&](Intersection> &argA, Intersection> &argB) . bool {
	// 		return argA._fieldTime < argB._fieldTime;
	// 	};
	// 	setIntersection(t, argObject);
	// 	//mbrIntersections.Add(Intersection(t, std::make_unique<Form>(*argObject)));
	//     sort(mbrIntersections.begin(), mbrIntersections.end(), funcComp);
	// }
	public void SetIntersect(double time, Form argObject)
	{
		SetIntersection(time, argObject);
		_fieldIntersections.Sort(delegate(Intersection argA, Intersection argB) {
			return argA._fieldTime.CompareTo(argB._fieldTime);
		});
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
			//Console.WriteLine("hit missed");
			Intersection varIx = new Intersection();
			varIx._fieldExists = false;
			return varIx;
		}
		else {
			//Console.WriteLine("hit");
			return _fieldIntersections[index];
		}
	}
	protected void SetIntersection(Intersection argIx) {
		_fieldIntersections.Add(argIx);
	}
    protected void SetIntersection(double argTime, Form argObject) {
		_fieldIntersections.Add(new Intersection(argTime, argObject));
	}
};