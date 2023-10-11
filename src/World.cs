using LibForm;
using LibLight;
using LibRay;
using LibColor;
using LibTuple;
using LibIntersection;
using LibMaterial;
using LibComparinator;
using LibMatrix;
namespace LibWorld;

public class World {
    public List<Form> mbrObjects;
    public List<PointSource> mbrLights;
    public World() 
    {
        mbrObjects = new List<Form>();
		mbrLights = new List<PointSource>();
    }
    public World(World argOther)
    {
        mbrObjects = new List<Form>();
        for (int i = 0; i < argOther.mbrObjects.Count(); ++i)
        {
            setObject(argOther.mbrObjects[i]);
            // mbrObjects.Add(std::make_unique<Form>(argOther.mbrObjects[i]));
        }
		mbrLights = new List<PointSource>();
        mbrLights = argOther.mbrLights;
    }
    // public World operator=(World argOther)
    // {
    //     if (this == argOther) return this;
    //     mbrObjects.Clear();
    //     for (int i = 0; i < argOther.mbrObjects.Count(); ++i)
    //     {
    //         setObject(argOther.mbrObjects[i]);
    //         //mbrObjects.Add(std::make_unique<Form>(argOther.mbrObjects[i]));
    //     }
    //     mbrLights = argOther.mbrLights;
    //     return this;
    // }
    public Intersections getIntersect(Ray argRay)
    {
        Intersections hits = new Intersections();
        for (int i = 0; i < mbrObjects.Count(); ++i)
        {
            Intersections varIx = mbrObjects[i].GetIntersections(argRay);
            for (int j = 0; j < varIx.mbrIntersections.Count(); ++j)
            {
                hits.intersect(varIx.mbrIntersections[j].mbrTime, varIx.mbrIntersections[j].mbrObject);
            }
        }
        return hits;
    }
    public Color GetColor(Ray r, int argLimit = 5)
    {
        // std::cout << "getColor( #:" << argLimit << " )" << std::endl;
        Intersections xs = getIntersect(r);
        Intersection hit = xs.hit();
        // if (!hit.mbrExists) return Color(50,205,50);
        if (!hit.mbrExists) return new Color(0,0,0);
        IntersectionState varIs = hit.getState(r, xs.mbrIntersections);
        // std::cout << "getColor()::is.mbrPoint(x:" << is.mbrPoint.mbrX << ",y:" << is.mbrPoint.mbrY << ",z:" << is.mbrPoint.mbrZ << ",w:" << is.mbrPoint.mbrW << std::endl;
        // std::cout << "getColor()::is.mbrOverPoint(x:" << is.mbrOverPoint.mbrX << ",y:" << is.mbrOverPoint.mbrY << ",z:" << is.mbrOverPoint.mbrZ << ",w:" << is.mbrOverPoint.mbrW << std::endl;
        // std::cout << "getColor()::is.mbrEye(x:" << is.mbrEye.mbrX << ",y:" << is.mbrEye.mbrY << ",z:" << is.mbrEye.mbrZ << ",w:" << is.mbrEye.mbrW << std::endl;
        // std::cout << "getColor()::is.mbrNormal(x:" << is.mbrNormal.mbrX << ",y:" << is.mbrNormal.mbrY << ",z:" << is.mbrNormal.mbrZ << ",w:" << is.mbrNormal.mbrW << std::endl;
        return getColorShaded(varIs, argLimit);
    }

    public Color getColorLighting(IntersectionState argIxState, int argLimit = 5)
    {
        if (argLimit < 1) return new Color(0,0,0);
        bool varInShadow = checkShadowed(argIxState.mbrOverPoint);
        Color varShade = new Color(0,0,0);
        for (int i = 0; i < mbrLights.Count();++i)
        {
            varShade = varShade + argIxState.mbrObject.GetColor(mbrLights[i], argIxState.mbrPoint, argIxState.mbrEye, argIxState.mbrNormal, varInShadow);
            // varShade = varShade + argIxState.mbrObject.getColor(mbrLights[i], argIxState.mbrOverPoint, argIxState.mbrEye, argIxState.mbrNormal, varInShadow);
        }
        return varShade;
    }
    public Color getColorShaded(IntersectionState argIxState, int argLimit = 5)
    {
        // std::cout << "getColorShaded( #:" << argLimit << ", t: " << argIxState.mbrTime << " )" << std::endl;
        if (argLimit < 1) return new Color(0,0,0);
        // std::cout << "getColorShaded()::argIxState.mbrPoint(x:" << argIxState.mbrPoint.mbrX << ",y:" << argIxState.mbrPoint.mbrY << ",z:" << argIxState.mbrPoint.mbrZ << ",w:" << argIxState.mbrPoint.mbrW << std::endl;
        // std::cout << "getColorShaded()::argIxState.mbrOverPoint(x:" << argIxState.mbrOverPoint.mbrX << ",y:" << argIxState.mbrOverPoint.mbrY << ",z:" << argIxState.mbrOverPoint.mbrZ << ",w:" << argIxState.mbrOverPoint.mbrW << std::endl;
        // std::cout << "getColorShaded()::argIxState.mbrEye(x:" << argIxState.mbrEye.mbrX << ",y:" << argIxState.mbrEye.mbrY << ",z:" << argIxState.mbrEye.mbrZ << ",w:" << argIxState.mbrEye.mbrW << std::endl;
        // std::cout << "getColorShaded()::argIxState.mbrNormal(x:" << argIxState.mbrNormal.mbrX << ",y:" << argIxState.mbrNormal.mbrY << ",z:" << argIxState.mbrNormal.mbrZ << ",w:" << argIxState.mbrNormal.mbrW << std::endl;
        // argIxState.renderConsole();
        bool varInShadow = checkShadowed(argIxState.mbrOverPoint);
        Color varDiffuse = new Color(0,0,0);
        for (int i = 0; i < mbrLights.Count();++i)
        {
            // varDiffuse = varDiffuse + argIxState.mbrObject.getColor(mbrLights[i], argIxState.mbrPoint, argIxState.mbrEye, argIxState.mbrNormal, varInShadow);
            varDiffuse = varDiffuse + argIxState.mbrObject.GetColor(mbrLights[i], argIxState.mbrOverPoint, argIxState.mbrEye, argIxState.mbrNormal, varInShadow);
        }
        // std::cout << "getColorShaded()::Diffuse(r:" << varDiffuse.mbrRed << ",g:" << varDiffuse.mbrGreen << ",b:" << varDiffuse.mbrBlue << std::endl;
        Color varReflect = getColorReflect(argIxState, argLimit);
        // std::cout << "getColorShaded()::Reflect(r:" << varReflect.mbrRed << ",g:" << varReflect.mbrGreen << ",b:" << varReflect.mbrBlue << std::endl;
        Color varRefract = getColorRefracted(argIxState, argLimit);
        // std::cout << "getColorShaded()::Refract(r:" << varRefract.mbrRed << ",g:" << varRefract.mbrGreen << ",b:" << varRefract.mbrBlue << std::endl;
        Color varRes = varDiffuse + varReflect + varRefract;
        // std::cout << "getColorShaded().Color(r:" << varRes.mbrRed << ",g:" << varRes.mbrGreen << ",b:" << varRes.mbrBlue << ")" << std::endl;
        // return varDiffuse + varReflect;
        return varRes;
    }
    public Color getColorReflect(IntersectionState argIntersectionState, int argLimit = 5) {
        // std::cout << "getColorReflect( #:" << argLimit << ", t: " << argIntersectionState.mbrTime << " )" << std::endl;
        // argIntersectionState.renderConsole();
        // std::cout << "getColorShaded()::argIntersectionState.mbrPoint(x:" << argIntersectionState.mbrPoint.mbrX << ",y:" << argIntersectionState.mbrPoint.mbrY << ",z:" << argIntersectionState.mbrPoint.mbrZ << ",w:" << argIntersectionState.mbrPoint.mbrW << std::endl;
        // std::cout << "getColorShaded()::argIntersectionState.mbrOverPoint(x:" << argIntersectionState.mbrOverPoint.mbrX << ",y:" << argIntersectionState.mbrOverPoint.mbrY << ",z:" << argIntersectionState.mbrOverPoint.mbrZ << ",w:" << argIntersectionState.mbrOverPoint.mbrW << std::endl;
        // std::cout << "getColorShaded()::argIntersectionState.mbrEye(x:" << argIntersectionState.mbrEye.mbrX << ",y:" << argIntersectionState.mbrEye.mbrY << ",z:" << argIntersectionState.mbrEye.mbrZ << ",w:" << argIntersectionState.mbrEye.mbrW << std::endl;
        // std::cout << "getColorShaded()::argIntersectionState.mbrNormal(x:" << argIntersectionState.mbrNormal.mbrX << ",y:" << argIntersectionState.mbrNormal.mbrY << ",z:" << argIntersectionState.mbrNormal.mbrZ << ",w:" << argIntersectionState.mbrNormal.mbrW << std::endl;
        if (argLimit < 1) return new Color(0,0,0);
        Comparinator varComp = new Comparinator();
        if (varComp.CheckFloat(argIntersectionState.mbrObject._fieldMaterial.mbrReflective, 0)) return new Color(0,0,0);
        Ray varRayReflect = new Ray(argIntersectionState.mbrOverPoint, argIntersectionState.mbrReflect);
        // std::cout << "getColorShaded()::varRayReflect.mbrOrigin(x:" << varRayReflect.mbrOrigin.mbrX << ",y:" << varRayReflect.mbrOrigin.mbrY << ",z:" << varRayReflect.mbrOrigin.mbrZ << ",w:" << varRayReflect.mbrOrigin.mbrW << std::endl;
        // std::cout << "getColorShaded()::varRayReflect.mbrDirection(x:" << varRayReflect.mbrDirection.mbrX << ",y:" << varRayReflect.mbrDirection.mbrY << ",z:" << varRayReflect.mbrDirection.mbrZ << ",w:" << varRayReflect.mbrDirection.mbrW << std::endl;
        Color varColor = GetColor(varRayReflect, argLimit - 1);
        // std::cout << "getColorReflect()::Color(r:" << varColor.mbrRed << ",g:" << varColor.mbrGreen << ",b:" << varColor.mbrBlue << ")" << " mbrReflective(" << argIntersectionState.mbrObject.mbrMaterial.mbrReflective << std::endl;
        // return varColor * argIntersectionState.mbrObject._fieldMaterial.mbrReflective;;
        Color varRes = varColor * argIntersectionState.mbrObject._fieldMaterial.mbrReflective;
        // std::cout << "getColorReflect()::Color(r:" << varRes.mbrRed << ",g:" << varRes.mbrGreen << ",b:" << varRes.mbrBlue << ")" << std::endl;
        return varRes;
    }
    public Color getColorRefracted(IntersectionState argIntersectionState, int argLimit = 5) {
        // std::cout << "getColorRefracted( #:" << argLimit << ", t: " << argIntersectionState.mbrTime << " )" << std::endl;
        // argIntersectionState.renderConsole("Color getColorRefracted");
        if (argLimit < 1) { return new Color(0,0,0); }
        Comparinator varComp = new Comparinator();
        if (varComp.CheckFloat(argIntersectionState.mbrObject._fieldMaterial.mbrTransparency, 0)) { return new Color(0,0,0); }
        double varNToN = argIntersectionState.mbrRefractiveIndexOne/argIntersectionState.mbrRefractiveIndexTwo;
        double varCosThetaI = argIntersectionState.mbrEye.GetDotProduct(argIntersectionState.mbrNormal);
        double varSinThetaTSquared = varNToN*varNToN * (1.0-(varCosThetaI*varCosThetaI));
        // std::cout << "getColorRefracted()::varNToN=(" << varNToN << std::endl;
        // std::cout << "getColorRefracted()::varCosThetaI=(" << varCosThetaI << std::endl;
        // std::cout << "getColorRefracted()::varSinThetaTSquared=(" << varSinThetaTSquared << std::endl;
        if (varSinThetaTSquared > 1) { return new Color(0,0,0); }
        double varCosThetaT = Math.Sqrt(1.0-varSinThetaTSquared);
        Vector varRefractDirection = (argIntersectionState.mbrNormal * ((varNToN * varCosThetaI) - varCosThetaT)) - (argIntersectionState.mbrEye * varNToN);
        // std::cout << "getColorRefracted()::varRefractDirection(x:" << varRefractDirection.mbrX << ",y:" << varRefractDirection.mbrY << ",z:" << varRefractDirection.mbrZ << ",w:" << varRefractDirection.mbrW << std::endl;
        Ray varRefractRay = new Ray(argIntersectionState.mbrUnderPoint, varRefractDirection);
        // std::cout << "getColorRefracted()::argIntersectionState.mbrUnderPoint(x:" << argIntersectionState.mbrUnderPoint.mbrX << ",y:" << argIntersectionState.mbrUnderPoint.mbrY << ",z:" << argIntersectionState.mbrUnderPoint.mbrZ << ",w:" << argIntersectionState.mbrUnderPoint.mbrW << std::endl;
        Color varRefractColor = GetColor(varRefractRay, argLimit-1) * argIntersectionState.mbrObject._fieldMaterial.mbrTransparency;
        // std::cout << "getColorRefracted().Color(r:" << varRefractColor.mbrRed << ",g:" << varRefractColor.mbrGreen << ",b:" << varRefractColor.mbrBlue << ")" << std::endl;
        return varRefractColor;
    }
    public bool checkShadowed(Point argPoint) {
        bool varFlagShadow = false;
        for (int i = 0; i < mbrLights.Count(); ++i)
        {
            Vector varDirection = mbrLights[i].mbrPosition - argPoint;
            double varDistance = varDirection.GetMagnitude();
            Vector varDirectionNormalized = varDirection.GetNormal();
            Ray varRay = new Ray (argPoint, varDirectionNormalized);
            Intersection varHit = getIntersect(varRay).hit();
            bool varShadow = varHit.mbrExists && (varHit.mbrTime < varDistance);
            varFlagShadow = varShadow ? true : varFlagShadow;
        }
        return varFlagShadow;
    }
    public void setObject(Form argObject) {
        mbrObjects.Add(argObject);
        // if (Sphere varSphere = dynamic_cast<Sphere >(argObject))
        // {
        //     mbrObjects.Add(std::make_unique<Sphere>(varSphere));
        // }
        // else if (Plane varPlane = dynamic_cast<Plane >(argObject))
        // {
        //     mbrObjects.Add(std::make_unique<Plane>(varPlane));
        // }
        // else {
        //     mbrObjects.Add(std::make_unique<Form>(argObject));
        // }
    }
    // void setObject(Form> argObject) {
    //     //mbrObjects.Add(std::make_unique<Form>(argObject));
    //     mbrObjects.Add(std::move(argObject));
    // }
    public void setLight(PointSource argLight) {
        mbrLights.Add(argLight);
    }
};

public class DefaultWorld : World {
    public DefaultWorld()
    {
        PointSource varDefaultLight = new PointSource(new Point(-10,10,-10), new Color(1,1,1));
        setLight(varDefaultLight);
        Sphere s = new Sphere();
        s.SetMaterial(new Material());
        s._fieldMaterial.mbrColor = new Color (0.8,1.0,0.6);
        s._fieldMaterial.mbrDiffuse = 0.7;
        s._fieldMaterial.mbrSpecular = 0.2;
        Sphere t = new Sphere();
        t.SetTransform(new ScalingMatrix(0.5,0.5,0.5));
        setObject(s);
        setObject(t);
        // mbrObjects.Add(std::make_unique<Sphere>(s));
        // mbrObjects.Add(std::make_unique<Sphere>(t));
    }
};

