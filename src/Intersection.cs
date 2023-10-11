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
	// 	mbrTime = argOther.mbrTime;
	// 	mbrObject = std::move(argOther.mbrObject);
	// 	argOther.mbrObject = nullptr;
	// 	mbrPoint = argOther.mbrPoint;
	// 	mbrEye = argOther.mbrEye;
	// 	mbrNormal = argOther.mbrNormal;
	// 	mbrInside = argOther.mbrInside;
	// 	mbrOverPoint = argOther.mbrOverPoint;
	// 	mbrReflect = argOther.mbrReflect;
	// 	mbrOverPoint = argOther.mbrOverPoint;
	// 	mbrRefractiveIndexOne = argOther.mbrRefractiveIndexOne;
	// 	mbrRefractiveIndexTwo = argOther.mbrRefractiveIndexTwo;
	// }
	// IntersectionState& IntersectionState::operator=(IntersectionState &argOther) {
	// 	if (this == &argOther) return *this;
	// 	mbrTime = argOther.mbrTime;
	// 	setObject(argOther.mbrObject);
	// 	mbrPoint = argOther.mbrPoint;
	// 	mbrEye = argOther.mbrEye;
	// 	mbrNormal = argOther.mbrNormal;
	// 	mbrInside = argOther.mbrInside;
	// 	mbrOverPoint = argOther.mbrOverPoint;
	// 	mbrReflect = argOther.mbrReflect;
	// 	mbrOverPoint = argOther.mbrOverPoint;
	// 	mbrRefractiveIndexOne = argOther.mbrRefractiveIndexOne;
	// 	mbrRefractiveIndexTwo = argOther.mbrRefractiveIndexTwo;
	// 	return *this;
	// }
	// IntersectionState& IntersectionState::operator=(IntersectionState &&argOther) noexcept {
	// 	if (this == &argOther) return *this;
	// 	mbrTime = argOther.mbrTime;
	// 	mbrObject = std::move(argOther.mbrObject);
	// 	argOther.mbrObject = nullptr;
	// 	mbrPoint = argOther.mbrPoint;
	// 	mbrEye = argOther.mbrEye;
	// 	mbrNormal = argOther.mbrNormal;
	// 	mbrInside = argOther.mbrInside;
	// 	mbrOverPoint = argOther.mbrOverPoint;
	// 	mbrReflect = argOther.mbrReflect;
	// 	mbrOverPoint = argOther.mbrOverPoint;
	// 	mbrRefractiveIndexOne = argOther.mbrRefractiveIndexOne;
	// 	mbrRefractiveIndexTwo = argOther.mbrRefractiveIndexTwo;
	// 	return *this;
	// }
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
	// 	mbrObject = std::move(argOther.mbrObject);
	// 	argOther.mbrObject = nullptr;
	// 	mbrTime = argOther.mbrTime;
	// 	mbrExists = argOther.mbrExists;
	// }
	// Intersection& Intersection::operator=(Intersection &argOther){
	// 	if (this == &argOther) return *this;
	// 	setObject(argOther.mbrObject);
	// 	mbrTime = argOther.mbrTime;
	// 	mbrExists = argOther.mbrExists;
	// 	return *this;
	// }
	// Intersection& Intersection::operator=(Intersection &&argOther) noexcept {
	// 	if (this == &argOther) return *this;
	// 	mbrObject = std::move(argOther.mbrObject);
	// 	argOther.mbrObject = nullptr;
	// 	mbrTime = argOther.mbrTime;
	// 	mbrExists = argOther.mbrExists;
	// 	return *this;
	// }
	public bool CheckEqual(Intersection argOther)
	{
		return _fieldTime == argOther._fieldTime && _fieldObject.CheckEqual(argOther._fieldObject);
	}

	// IntersectionState Intersection::getState(Ray argRay)
	// {
	// 	IntersectionState is = IntersectionState();
	// 	is.mbrTime = mbrTime;
	// 	is.setObject(mbrObject);
	// 	is.mbrPoint = argRay.getPosition(is.mbrTime);
	// 	is.mbrEye = -(argRay.mbrDirection);
	// 	is.mbrNormal = is.mbrObject.getNormal(is.mbrPoint);
	// 	if (is.mbrNormal.dot(is.mbrEye) < 0) {
	// 		is.mbrInside = true;
	// 		is.mbrNormal = -is.mbrNormal;
	// 	}
	// 	else {
	// 		is.mbrInside = false;
	// 	}
	// 	is.mbrOverPoint = is.mbrPoint + is.mbrNormal * getEpsilon();
	// 	is.mbrReflect = argRay.mbrDirection.reflect(is.mbrNormal);
	// 	return is;
	// }
	// IntersectionState Intersection::getState(Ray argRay, Intersections &argIntersections)
	// {
	// 	IntersectionState is = IntersectionState();
	// 	is.mbrTime = mbrTime;
	// 	is.setObject(mbrObject);
	// 	is.mbrPoint = argRay.getPosition(is.mbrTime);
	// 	is.mbrEye = -(argRay.mbrDirection);
	// 	is.mbrNormal = is.mbrObject.getNormal(is.mbrPoint);
	// 	if (is.mbrNormal.dot(is.mbrEye) < 0) {
	// 		is.mbrInside = true;
	// 		is.mbrNormal = -is.mbrNormal;
	// 	}
	// 	else {
	// 		is.mbrInside = false;
	// 	}
	// 	is.mbrOverPoint = is.mbrPoint + is.mbrNormal * getEpsilon();
	// 	is.mbrReflect = argRay.mbrDirection.reflect(is.mbrNormal);
	// 	List<Form*> varHitObjects;
	// 	// Intersection varHit = argIntersections.hit();
	// 	Intersection varHit = Intersection(is.mbrTime,mbrObject);
	// 	for (int i = 0; i < argIntersections.mbrIntersections.Count(); ++i) {
	// 		if (varHit.checkEqual(*argIntersections.mbrIntersections[i])) {
	// 			if (varHitObjects.Count() == 0) { is.mbrRefractiveIndexOne = 1.0; }
	// 			else { is.mbrRefractiveIndexOne = varHitObjects.back().mbrMaterial.mbrRefractiveIndex; }
	// 		}
	// 		int varFound = -1;
	// 		List<Form*>::iterator varItr = varHitObjects.begin();
	// 		for (; varItr != varHitObjects.end(); ++varItr)
	// 		{
	// 			if (*varItr == varHit.mbrObject) {
	// 				varHitObjects.erase(varItr);
	// 				varFound = 1;
	// 				break;
	// 			}
	// 			// if (varHitObjects[j] == varHit.mbrObject)
	// 			// {
	// 			// 	varFound = j;
	// 			// 	break;
	// 			// }
	// 		}
	// 		// if (varFound > 0) {varHitObjects.erase(varHitObjects.begin()+varFound);}
	// 		// else {varHitObjects.Add(varHit.mbrObject);}
	// 		if (varFound < 0) {varHitObjects.Add(varHit.mbrObject);}
	// 		if (varHit.checkEqual(*argIntersections.mbrIntersections[i])) {
	// 			if (varHitObjects.empty()) { is.mbrRefractiveIndexTwo = 1.0; }
	// 			else { is.mbrRefractiveIndexTwo = varHitObjects.back().mbrMaterial.mbrRefractiveIndex; }
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
		// Console.WriteLine("Intersection::getState()::varIs.mbrTime=(" << varIs.mbrTime);
		varIs.SetObject(_fieldObject);
		varIs._fieldPoint = argRay.GetPosition(varIs._fieldTime);
		// Console.WriteLine("Intersection::getState()::varIs.mbrPoint=(x:" << varIs.mbrPoint.mbrX << ",y:" << varIs.mbrPoint.mbrY << ",z:" << varIs.mbrPoint.mbrZ << ",w:" << varIs.mbrPoint.mbrW);
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
		// Console.WriteLine("Intersection::getState()::varIs.mbrOverPoint=(x:" << varIs.mbrOverPoint.mbrX << ",y:" << varIs.mbrOverPoint.mbrY << ",z:" << varIs.mbrOverPoint.mbrZ << ",w:" << varIs.mbrOverPoint.mbrW;
		// Console.WriteLine(" = Intersection::getState()::varIs.mbrNormal=(x:" << varIs.mbrNormal.mbrX << ",y:" << varIs.mbrNormal.mbrY << ",z:" << varIs.mbrNormal.mbrZ << ",w:" << varIs.mbrNormal.mbrW);
		varIs._fieldReflect = argRay._fieldDirection.GetReflect(varIs._fieldNormal);
		List<Form> varHitObjects = new List<Form>();
		// Intersection varHit = argIntersections.hit();
		Intersection varHit = new Intersection(varIs._fieldTime, _fieldObject);
		// Console.WriteLine("Intersection::getState()::hit.mbrTime(" << varHit.mbrTime);
		for (int i = 0; i < argIntersections.Count(); ++i) {
			// Console.WriteLine("Intersection::getState()::argIntersections[i].mbrTime(" << argIntersections[i].mbrTime);
			// Console.WriteLine("Intersection::getState()::argIntersections[i].mbrObject.mbrMaterial.mbrRefractiveIndex(" << argIntersections[i].mbrObject.mbrMaterial.mbrRefractiveIndex);
			if (varHit.CheckEqual(argIntersections[i])) {
				if (varHitObjects.Count() == 0) { varIs._fieldRefractiveIndexOne = 1.0; }
				else { varIs._fieldRefractiveIndexOne = varHitObjects[varHitObjects.Count()-1]._fieldMaterial.mbrRefractiveIndex; }
			}
			int varFound = -1;
			for (int j = 0; j < varHitObjects.Count(); j++)
			{
				if (varHitObjects[j].CheckEqual(argIntersections[i]._fieldObject)) {
					// Console.WriteLine("Intersection::getState()::erase(argIntersections[i].mbrObject(" << argIntersections[i].mbrObject.mbrMaterial.mbrRefractiveIndex);
					varHitObjects.RemoveAt(j);
					varFound = 1;
					break;
				}
				// if (varHitObjects[j] == varHit.mbrObject)
				// {
				// 	varFound = j;
				// 	break;
				// }
			}
			// if (varFound > 0) {varHitObjects.erase(varHitObjects.begin()+varFound);}
			// else {varHitObjects.Add(varHit.mbrObject);}
			if (varFound < 0) {
				varHitObjects.Add(argIntersections[i]._fieldObject);
				// Console.WriteLine("Intersection::getState()::varHitObjects.back().mbrMaterial.mbrRefractiveIndex(" << varHitObjects.back().mbrMaterial.mbrRefractiveIndex);
			}
			if (varHit.CheckEqual(argIntersections[i])) {
				if (varHitObjects.Count == 0) { varIs._fieldRefractiveIndexTwo = 1.0; }
				else { varIs._fieldRefractiveIndexTwo = varHitObjects[varHitObjects.Count-1]._fieldMaterial.mbrRefractiveIndex; }
				break;
			}
			// Console.WriteLine("Intersection::getState()::varIs.mbrRefractiveIndexOne" << varIs.mbrRefractiveIndexOne);
			// Console.WriteLine("Intersection::getState()::varIs.mbrRefractiveIndexTwo" << varIs.mbrRefractiveIndexTwo);
		}
		// Console.WriteLine("Intersection::getState()::varIs.mbrRefractiveIndexOne" << varIs.mbrRefractiveIndexOne);
		// Console.WriteLine("Intersection::getState()::varIs.mbrRefractiveIndexTwo" << varIs.mbrRefractiveIndexTwo);
		// varIs.mbrUnderPoint = varIs.mbrPoint.subtract(varIs.mbrNormal * getEpsilon());
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
			// Intersection varIs = other.mbrIntersections[i];
			// mbrIntersections.Add(varIs);
		}
		//mbrIntersections = other.mbrIntersections;
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
	// 	for (int i = 0; i < other.mbrIntersections.Count(); ++i)
	// 	{
	// 		setIntersection(other.mbrIntersections[i]);
	// 		// Intersection varIs = other.mbrIntersections[i];
	// 		// mbrIntersections.Add(varIs);
	// 	}
	// 	// mbrIntersections = other.mbrIntersections;
	// 	return *this;
	// }
	// void Intersections::intersect(double t, Form> argObject)
	// {
	// 	auto funcComp = [&](Intersection> &argA, Intersection> &argB) . bool {
	// 		return argA.mbrTime < argB.mbrTime;
	// 	};
	// 	setIntersection(t, argObject);
	// 	//mbrIntersections.Add(Intersection(t, std::make_unique<Form>(*argObject)));
	//     sort(mbrIntersections.begin(), mbrIntersections.end(), funcComp);
	// }
	public void setIntersect(double time, Form argObject)
	{
		SetIntersection(time, argObject);
		_fieldIntersections.Sort(delegate(Intersection argA, Intersection argB) {
			return argA._fieldTime < argB._fieldTime ? 1 : -1;
		});
	}
	public Intersection getHit() {
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
	public void SetIntersection(Intersection argIx) {
		_fieldIntersections.Add(argIx);
	}
    public void SetIntersection(double argTime, Form argObject) {
		_fieldIntersections.Add(new Intersection(argTime, argObject));
	}
};