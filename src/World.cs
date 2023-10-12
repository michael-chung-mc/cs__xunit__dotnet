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
    public List<Form> _fieldObjects;
    public List<PointSource> _fieldLights;
    public World() 
    {
        _fieldObjects = new List<Form>();
		_fieldLights = new List<PointSource>();
    }
    public World(World argOther)
    {
        _fieldObjects = new List<Form>();
        for (int i = 0; i < argOther._fieldObjects.Count(); ++i)
        {
            SetObject(argOther._fieldObjects[i]);
            // mbrObjects.Add(std::make_unique<Form>(argOther.mbrObjects[i]));
        }
		_fieldLights = new List<PointSource>();
        _fieldLights = argOther._fieldLights;
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
    public Intersections GetIntersect(Ray argRay)
    {
        Intersections hits = new Intersections();
        for (int i = 0; i < _fieldObjects.Count(); ++i)
        {
            Intersections varIx = _fieldObjects[i].GetIntersections(argRay);
            for (int j = 0; j < varIx._fieldIntersections.Count(); ++j)
            {
                hits.SetIntersect(varIx._fieldIntersections[j]._fieldTime, varIx._fieldIntersections[j]._fieldObject);
            }
        }
        return hits;
    }
    public Color GetColor(Ray argRay, int argLimit = 5)
    {
        // std::cout << "getColor( #:" << argLimit << " )" << std::endl;
        Intersections xs = GetIntersect(argRay);
        Intersection hit = xs.GetHit();
        // if (!hit.mbrExists) return Color(50,205,50);
        if (!hit._fieldExists) return new Color(0,0,0);
        IntersectionState varIs = hit.GetState(argRay, xs._fieldIntersections);
        // std::cout << "getColor()::is.mbrPoint(x:" << is.mbrPoint.mbrX << ",y:" << is.mbrPoint.mbrY << ",z:" << is.mbrPoint.mbrZ << ",w:" << is.mbrPoint.mbrW << std::endl;
        // std::cout << "getColor()::is.mbrOverPoint(x:" << is.mbrOverPoint.mbrX << ",y:" << is.mbrOverPoint.mbrY << ",z:" << is.mbrOverPoint.mbrZ << ",w:" << is.mbrOverPoint.mbrW << std::endl;
        // std::cout << "getColor()::is.mbrEye(x:" << is.mbrEye.mbrX << ",y:" << is.mbrEye.mbrY << ",z:" << is.mbrEye.mbrZ << ",w:" << is.mbrEye.mbrW << std::endl;
        // std::cout << "getColor()::is.mbrNormal(x:" << is.mbrNormal.mbrX << ",y:" << is.mbrNormal.mbrY << ",z:" << is.mbrNormal.mbrZ << ",w:" << is.mbrNormal.mbrW << std::endl;
        return GetColorShaded(varIs, argLimit);
    }

    public Color GetColorLighting(IntersectionState argIxState, int argLimit = 5)
    {
        if (argLimit < 1) return new Color(0,0,0);
        bool varInShadow = CheckShadowed(argIxState._fieldOverPoint);
        Color varDiffuse = new Color(0,0,0);
        for (int i = 0; i < _fieldLights.Count();++i)
        {
            varDiffuse = varDiffuse + argIxState._fieldObject.GetColor(_fieldLights[i], argIxState._fieldOverPoint, argIxState._fieldPov, argIxState._fieldNormal, varInShadow);
        }
        return varDiffuse;
    }
    public Color GetColorShaded(IntersectionState argIxState, int argLimit = 5)
    {
        // std::cout << "getColorShaded( #:" << argLimit << ", t: " << argIxState.mbrTime << " )" << std::endl;
        if (argLimit < 1) return new Color(0,0,0);
        // std::cout << "getColorShaded()::argIxState.mbrPoint(x:" << argIxState.mbrPoint.mbrX << ",y:" << argIxState.mbrPoint.mbrY << ",z:" << argIxState.mbrPoint.mbrZ << ",w:" << argIxState.mbrPoint.mbrW << std::endl;
        // std::cout << "getColorShaded()::argIxState.mbrOverPoint(x:" << argIxState.mbrOverPoint.mbrX << ",y:" << argIxState.mbrOverPoint.mbrY << ",z:" << argIxState.mbrOverPoint.mbrZ << ",w:" << argIxState.mbrOverPoint.mbrW << std::endl;
        // std::cout << "getColorShaded()::argIxState.mbrEye(x:" << argIxState.mbrEye.mbrX << ",y:" << argIxState.mbrEye.mbrY << ",z:" << argIxState.mbrEye.mbrZ << ",w:" << argIxState.mbrEye.mbrW << std::endl;
        // std::cout << "getColorShaded()::argIxState.mbrNormal(x:" << argIxState.mbrNormal.mbrX << ",y:" << argIxState.mbrNormal.mbrY << ",z:" << argIxState.mbrNormal.mbrZ << ",w:" << argIxState.mbrNormal.mbrW << std::endl;
        // argIxState.renderConsole();
        bool varInShadow = CheckShadowed(argIxState._fieldOverPoint);
        Color varDiffuse = new Color(0,0,0);
        for (int i = 0; i < _fieldLights.Count();++i)
        {
            // varDiffuse = varDiffuse + argIxState.mbrObject.getColor(mbrLights[i], argIxState.mbrPoint, argIxState.mbrEye, argIxState.mbrNormal, varInShadow);
            varDiffuse = varDiffuse + argIxState._fieldObject.GetColor(_fieldLights[i], argIxState._fieldOverPoint, argIxState._fieldPov, argIxState._fieldNormal, varInShadow);
        }
        // std::cout << "getColorShaded()::Diffuse(r:" << varDiffuse.mbrRed << ",g:" << varDiffuse.mbrGreen << ",b:" << varDiffuse.mbrBlue << std::endl;
        Color varReflect = GetColorReflect(argIxState, argLimit);
        // std::cout << "getColorShaded()::Reflect(r:" << varReflect.mbrRed << ",g:" << varReflect.mbrGreen << ",b:" << varReflect.mbrBlue << std::endl;
        Color varRefract = GetColorRefracted(argIxState, argLimit);
        // std::cout << "getColorShaded()::Refract(r:" << varRefract.mbrRed << ",g:" << varRefract.mbrGreen << ",b:" << varRefract.mbrBlue << std::endl;
        Material varMat = argIxState._fieldObject._fieldMaterial;
        Color varRes = varDiffuse + varReflect + varRefract;
        if (varMat._fieldReflective > 0 && varMat._fieldTransparency > 0) {
            double varReflectance = argIxState.GetSchlick();
            varRes = varDiffuse + (varReflect * varReflectance) + (varRefract * (1-varReflectance));
        }
        // std::cout << "getColorShaded().Color(r:" << varRes.mbrRed << ",g:" << varRes.mbrGreen << ",b:" << varRes.mbrBlue << ")" << std::endl;
        // return varDiffuse + varReflect;
        return varRes;
    }
    public Color GetColorReflect(IntersectionState argIntersectionState, int argLimit = 5) {
        // std::cout << "getColorReflect( #:" << argLimit << ", t: " << argIntersectionState.mbrTime << " )" << std::endl;
        // argIntersectionState.renderConsole();
        // std::cout << "getColorShaded()::argIntersectionState.mbrPoint(x:" << argIntersectionState.mbrPoint.mbrX << ",y:" << argIntersectionState.mbrPoint.mbrY << ",z:" << argIntersectionState.mbrPoint.mbrZ << ",w:" << argIntersectionState.mbrPoint.mbrW << std::endl;
        // std::cout << "getColorShaded()::argIntersectionState.mbrOverPoint(x:" << argIntersectionState.mbrOverPoint.mbrX << ",y:" << argIntersectionState.mbrOverPoint.mbrY << ",z:" << argIntersectionState.mbrOverPoint.mbrZ << ",w:" << argIntersectionState.mbrOverPoint.mbrW << std::endl;
        // std::cout << "getColorShaded()::argIntersectionState.mbrEye(x:" << argIntersectionState.mbrEye.mbrX << ",y:" << argIntersectionState.mbrEye.mbrY << ",z:" << argIntersectionState.mbrEye.mbrZ << ",w:" << argIntersectionState.mbrEye.mbrW << std::endl;
        // std::cout << "getColorShaded()::argIntersectionState.mbrNormal(x:" << argIntersectionState.mbrNormal.mbrX << ",y:" << argIntersectionState.mbrNormal.mbrY << ",z:" << argIntersectionState.mbrNormal.mbrZ << ",w:" << argIntersectionState.mbrNormal.mbrW << std::endl;
        if (argLimit < 1) return new Color(0,0,0);
        Comparinator varComp = new Comparinator();
        if (varComp.CheckFloat(argIntersectionState._fieldObject._fieldMaterial._fieldReflective, 0)) return new Color(0,0,0);
        Ray varRayReflect = new Ray(argIntersectionState._fieldOverPoint, argIntersectionState._fieldReflect);
        // std::cout << "getColorShaded()::varRayReflect.mbrOrigin(x:" << varRayReflect.mbrOrigin.mbrX << ",y:" << varRayReflect.mbrOrigin.mbrY << ",z:" << varRayReflect.mbrOrigin.mbrZ << ",w:" << varRayReflect.mbrOrigin.mbrW << std::endl;
        // std::cout << "getColorShaded()::varRayReflect.mbrDirection(x:" << varRayReflect.mbrDirection.mbrX << ",y:" << varRayReflect.mbrDirection.mbrY << ",z:" << varRayReflect.mbrDirection.mbrZ << ",w:" << varRayReflect.mbrDirection.mbrW << std::endl;
        Color varColor = GetColor(varRayReflect, argLimit - 1);
        // std::cout << "getColorReflect()::Color(r:" << varColor.mbrRed << ",g:" << varColor.mbrGreen << ",b:" << varColor.mbrBlue << ")" << " mbrReflective(" << argIntersectionState.mbrObject.mbrMaterial.mbrReflective << std::endl;
        // return varColor * argIntersectionState.mbrObject._fieldMaterial.mbrReflective;;
        Color varRes = varColor * argIntersectionState._fieldObject._fieldMaterial._fieldReflective;
        // std::cout << "getColorReflect()::Color(r:" << varRes.mbrRed << ",g:" << varRes.mbrGreen << ",b:" << varRes.mbrBlue << ")" << std::endl;
        return varRes;
    }
    public Color GetColorRefracted(IntersectionState argIntersectionState, int argLimit = 5) {
        // std::cout << "getColorRefracted( #:" << argLimit << ", t: " << argIntersectionState.mbrTime << " )" << std::endl;
        // argIntersectionState.renderConsole("Color getColorRefracted");
        if (argLimit < 1) { return new Color(0,0,0); }
        Comparinator varComp = new Comparinator();
        if (varComp.CheckFloat(argIntersectionState._fieldObject._fieldMaterial._fieldTransparency, 0)) { return new Color(0,0,0); }
        double varNToN = argIntersectionState._fieldRefractiveIndexOne/argIntersectionState._fieldRefractiveIndexTwo;
        double varCosThetaI = argIntersectionState._fieldPov.GetDotProduct(argIntersectionState._fieldNormal);
        double varSinThetaTSquared = varNToN*varNToN * (1.0-(varCosThetaI*varCosThetaI));
        // std::cout << "getColorRefracted()::varNToN=(" << varNToN << std::endl;
        // std::cout << "getColorRefracted()::varCosThetaI=(" << varCosThetaI << std::endl;
        // std::cout << "getColorRefracted()::varSinThetaTSquared=(" << varSinThetaTSquared << std::endl;
        if (varSinThetaTSquared > 1) { return new Color(0,0,0); }
        double varCosThetaT = Math.Sqrt(1.0-varSinThetaTSquared);
        Vector varRefractDirection = (argIntersectionState._fieldNormal * ((varNToN * varCosThetaI) - varCosThetaT)) - (argIntersectionState._fieldPov * varNToN);
        // std::cout << "getColorRefracted()::varRefractDirection(x:" << varRefractDirection.mbrX << ",y:" << varRefractDirection.mbrY << ",z:" << varRefractDirection.mbrZ << ",w:" << varRefractDirection.mbrW << std::endl;
        Ray varRefractRay = new Ray(argIntersectionState._fieldUnderPoint, varRefractDirection);
        // std::cout << "getColorRefracted()::argIntersectionState.mbrUnderPoint(x:" << argIntersectionState.mbrUnderPoint.mbrX << ",y:" << argIntersectionState.mbrUnderPoint.mbrY << ",z:" << argIntersectionState.mbrUnderPoint.mbrZ << ",w:" << argIntersectionState.mbrUnderPoint.mbrW << std::endl;
        Color varRefractColor = GetColor(varRefractRay, argLimit-1) * argIntersectionState._fieldObject._fieldMaterial._fieldTransparency;
        // std::cout << "getColorRefracted().Color(r:" << varRefractColor.mbrRed << ",g:" << varRefractColor.mbrGreen << ",b:" << varRefractColor.mbrBlue << ")" << std::endl;
        return varRefractColor;
    }
    public bool CheckShadowed(Point argPoint) {
        bool varFlagShadow = false;
        for (int i = 0; i < _fieldLights.Count(); ++i)
        {
            Vector varDirection = _fieldLights[i].mbrPosition - argPoint;
            double varDistance = varDirection.GetMagnitude();
            Vector varDirectionNormalized = varDirection.GetNormal();
            Ray varRay = new Ray (argPoint, varDirectionNormalized);
            Intersection varHit = GetIntersect(varRay).GetHit();
            bool varShadow = varHit._fieldExists && (varHit._fieldTime < varDistance);
            varFlagShadow = varShadow ? true : varFlagShadow;
        }
        return varFlagShadow;
    }
    public void SetObject(Form argObject) {
        _fieldObjects.Add(argObject);
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
    public void SetLight(PointSource argLight) {
        _fieldLights.Add(argLight);
    }
};

public class DefaultWorld : World {
    public DefaultWorld()
    {
        PointSource varDefaultLight = new PointSource(new Point(-10,10,-10), new Color(1,1,1));
        SetLight(varDefaultLight);
        Sphere s = new Sphere();
        s.SetMaterial(new Material());
        s._fieldMaterial._fieldColor = new Color (0.8,1.0,0.6);
        s._fieldMaterial._fieldDiffuse = 0.7;
        s._fieldMaterial._fieldSpecular = 0.2;
        Sphere t = new Sphere();
        t.SetTransform(new ScalingMatrix(0.5,0.5,0.5));
        SetObject(s);
        SetObject(t);
        // mbrObjects.Add(std::make_unique<Sphere>(s));
        // mbrObjects.Add(std::make_unique<Sphere>(t));
    }
};

