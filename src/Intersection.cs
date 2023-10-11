using LibForm;
using LibTuple;
using LibRay;
using LibProjectMeta;
namespace LibIntersection;

public class IntersectionState {
	public double mbrTime;
	public Form mbrObject;
	public Point mbrPoint;
	public Point mbrOverPoint;
	public Point mbrUnderPoint;
	public Vector mbrEye;
	public Vector mbrNormal;
	public Vector mbrReflect;
	public bool mbrInside;
	public double mbrRefractiveIndexOne;
	public double mbrRefractiveIndexTwo;
	public IntersectionState(){
		mbrTime = 0;
		mbrObject = new Form();
		mbrPoint = new Point();
		mbrEye = new Vector();
		mbrNormal = new Vector();
		mbrInside = false;
		mbrUnderPoint = new Point();
		mbrReflect = new Vector();
		mbrOverPoint = new Point();
		mbrRefractiveIndexOne = 0.0;
		mbrRefractiveIndexTwo = 0.0;
	}
	public IntersectionState(IntersectionState argOther) {
		mbrTime = argOther.mbrTime;
		setObject(argOther.mbrObject);
		mbrPoint = argOther.mbrPoint;
		mbrEye = argOther.mbrEye;
		mbrNormal = argOther.mbrNormal;
		mbrInside = argOther.mbrInside;
		mbrOverPoint = argOther.mbrOverPoint;
		mbrReflect = argOther.mbrReflect;
		mbrOverPoint = argOther.mbrOverPoint;
		mbrRefractiveIndexOne = argOther.mbrRefractiveIndexOne;
		mbrRefractiveIndexTwo = argOther.mbrRefractiveIndexTwo;
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
    public void setObject(Form argObject) {
		mbrObject = argObject;
	}
	public void renderConsole() {
		Console.WriteLine("IntersectionState::renderConsole()");
		Console.WriteLine($"IntersectionState::mbrTime: {mbrTime}");
		Console.WriteLine("IntersectionState::mbrObject: ");
		mbrObject.RenderConsole();
		Console.WriteLine("IntersectionState::mbrPoint: ");
		mbrPoint.RenderConsole();
		Console.WriteLine("IntersectionState::mbrEye: ");
		mbrEye.RenderConsole();
		Console.WriteLine("IntersectionState::mbrNormal: ");
		mbrNormal.RenderConsole();
		Console.WriteLine($"IntersectionState::mbrInside: {mbrInside}");
		Console.WriteLine("IntersectionState::mbrOverPoint: ");
		mbrOverPoint.RenderConsole();
		Console.WriteLine("IntersectionState::mbrReflect: ");
		mbrReflect.RenderConsole();
	}
};

public class Intersection {
	public bool mbrExists;
	public double mbrTime;
	public Form mbrObject;
	public Intersection()
	{
		mbrObject = new Form();
		mbrTime = 0;
		mbrExists = true;
	}
	public Intersection(Intersection argOther){
		setObject(argOther.mbrObject);
		mbrTime = argOther.mbrTime;
		mbrExists = argOther.mbrExists;
	}
	// public Intersection(Intersection &&argOther) noexcept;
	// public Intersection(double time, Form *s)
	// {
	// 	setObject(s);
	// 	mbrTime = time;
	// 	mbrExists = true;
	// }
	public Intersection(double time, Form s)
	{
		setObject(s);
		mbrTime = time;
		mbrExists = true;
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
	public bool checkEqual(Intersection other)
	{
		return mbrTime == other.mbrTime && mbrObject.CheckEqual(other.mbrObject);
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
	public IntersectionState getState(Ray argRay, List<Intersection> argIntersections)
	{
		ProjectMeta varPM = new ProjectMeta();
		IntersectionState varIs = new IntersectionState();
		varIs.mbrTime = mbrTime;
		// Console.WriteLine("Intersection::getState()::varIs.mbrTime=(" << varIs.mbrTime);
		varIs.setObject(mbrObject);
		varIs.mbrPoint = argRay.GetPosition(varIs.mbrTime);
		// Console.WriteLine("Intersection::getState()::varIs.mbrPoint=(x:" << varIs.mbrPoint.mbrX << ",y:" << varIs.mbrPoint.mbrY << ",z:" << varIs.mbrPoint.mbrZ << ",w:" << varIs.mbrPoint.mbrW);
		varIs.mbrEye = -argRay._fieldDirection;
		varIs.mbrNormal = varIs.mbrObject.GetNormal(varIs.mbrPoint);
		if (varIs.mbrNormal.GetDotProduct(varIs.mbrEye) < 0) {
			varIs.mbrInside = true;
			varIs.mbrNormal = -varIs.mbrNormal;
		}
		else {
			varIs.mbrInside = false;
		}
		varIs.mbrOverPoint = varIs.mbrPoint + (varIs.mbrNormal * varPM.getEpsilon());
		// Console.WriteLine("Intersection::getState()::varIs.mbrOverPoint=(x:" << varIs.mbrOverPoint.mbrX << ",y:" << varIs.mbrOverPoint.mbrY << ",z:" << varIs.mbrOverPoint.mbrZ << ",w:" << varIs.mbrOverPoint.mbrW;
		// Console.WriteLine(" = Intersection::getState()::varIs.mbrNormal=(x:" << varIs.mbrNormal.mbrX << ",y:" << varIs.mbrNormal.mbrY << ",z:" << varIs.mbrNormal.mbrZ << ",w:" << varIs.mbrNormal.mbrW);
		varIs.mbrReflect = argRay._fieldDirection.GetReflect(varIs.mbrNormal);
		List<Form> varHitObjects = new List<Form>();
		// Intersection varHit = argIntersections.hit();
		Intersection varHit = new Intersection(varIs.mbrTime, mbrObject);
		// Console.WriteLine("Intersection::getState()::hit.mbrTime(" << varHit.mbrTime);
		for (int i = 0; i < argIntersections.Count(); ++i) {
			// Console.WriteLine("Intersection::getState()::argIntersections[i].mbrTime(" << argIntersections[i].mbrTime);
			// Console.WriteLine("Intersection::getState()::argIntersections[i].mbrObject.mbrMaterial.mbrRefractiveIndex(" << argIntersections[i].mbrObject.mbrMaterial.mbrRefractiveIndex);
			if (varHit.checkEqual(argIntersections[i])) {
				if (varHitObjects.Count() == 0) { varIs.mbrRefractiveIndexOne = 1.0; }
				else { varIs.mbrRefractiveIndexOne = varHitObjects[varHitObjects.Count()-1]._fieldMaterial.mbrRefractiveIndex; }
			}
			int varFound = -1;
			for (int j = 0; j < varHitObjects.Count(); j++)
			{
				if (varHitObjects[j].CheckEqual(argIntersections[i].mbrObject)) {
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
				varHitObjects.Add(argIntersections[i].mbrObject);
				// Console.WriteLine("Intersection::getState()::varHitObjects.back().mbrMaterial.mbrRefractiveIndex(" << varHitObjects.back().mbrMaterial.mbrRefractiveIndex);
			}
			if (varHit.checkEqual(argIntersections[i])) {
				if (varHitObjects.Count == 0) { varIs.mbrRefractiveIndexTwo = 1.0; }
				else { varIs.mbrRefractiveIndexTwo = varHitObjects[varHitObjects.Count-1]._fieldMaterial.mbrRefractiveIndex; }
				break;
			}
			// Console.WriteLine("Intersection::getState()::varIs.mbrRefractiveIndexOne" << varIs.mbrRefractiveIndexOne);
			// Console.WriteLine("Intersection::getState()::varIs.mbrRefractiveIndexTwo" << varIs.mbrRefractiveIndexTwo);
		}
		// Console.WriteLine("Intersection::getState()::varIs.mbrRefractiveIndexOne" << varIs.mbrRefractiveIndexOne);
		// Console.WriteLine("Intersection::getState()::varIs.mbrRefractiveIndexTwo" << varIs.mbrRefractiveIndexTwo);
		// varIs.mbrUnderPoint = varIs.mbrPoint.subtract(varIs.mbrNormal * getEpsilon());
		varIs.mbrUnderPoint = varIs.mbrPoint - (varIs.mbrNormal * varPM.getEpsilon());
		return varIs;
	}
	public void setObject(Form argObject) {
		mbrObject = argObject;
	}
}

public class Intersections {
    public List<Intersection> mbrIntersections;
	public Intersections()
	{
		mbrIntersections = new List<Intersection>();
		//Intersection i = Intersection(0, Sphere());
		//intersections.Add(i);
	}
	public Intersections(Intersections other)
	{
		mbrIntersections.Clear();
		for (int i = 0; i < other.mbrIntersections.Count(); ++i)
		{
			setIntersection(other.mbrIntersections[i]);
			// Intersection varIs = other.mbrIntersections[i];
			// mbrIntersections.Add(varIs);
		}
		//mbrIntersections = other.mbrIntersections;
	}
	public Intersections(double time, Form s)
	{
		setIntersection(time, s);
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
	public void intersect(double time, Form argObject)
	{
		setIntersection(time, argObject);
		mbrIntersections.Sort(delegate(Intersection argA, Intersection argB) {
			return argA.mbrTime < argB.mbrTime ? 1 : -1;
		});
	}
	public Intersection hit() {
		int index = -1;
		for (int i = 0; i < mbrIntersections.Count(); ++i)
		{
			if ((mbrIntersections[i].mbrTime > 0)
				&& ((index < 0) || (mbrIntersections[i].mbrTime <= mbrIntersections[index].mbrTime)))
			{
				index = i;
				break;
			}
		}
		if (index < 0) {
			//Console.WriteLine("hit missed");
			Intersection varIx = new Intersection();
			varIx.mbrExists = false;
			return varIx;
		}
		else {
			//Console.WriteLine("hit");
			return mbrIntersections[index];
		}
	}
	public void setIntersection(Intersection argIx) {
		mbrIntersections.Add(argIx);
	}
    public void setIntersection(double t, Form argObject) {
		mbrIntersections.Add(new Intersection(t, argObject));
	}
};