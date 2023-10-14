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
        }
		_fieldLights = new List<PointSource>();
        _fieldLights = argOther._fieldLights;
    }
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
        Intersections xs = GetIntersect(argRay);
        Intersection hit = xs.GetHit();
        if (!hit._fieldExists) return new Color(0,0,0);
        IntersectionState varIs = hit.GetState(argRay, xs._fieldIntersections);
        return GetColorShaded(varIs, argLimit);
    }

    // public Color GetColorLighting(IntersectionState argIxState, int argLimit = 5)
    // {
    //     Color varDiffuse = new Color(0,0,0);
    //     for (int i = 0; i < _fieldLights.Count();++i)
    //     {
    //         bool varInShadow = CheckShadowed(_fieldLights[i], argIxState._fieldOverPoint);
    //         varDiffuse = varDiffuse + argIxState._fieldObject.GetColorShaded(_fieldLights[i], argIxState._fieldOverPoint, argIxState._fieldPov, argIxState._fieldNormal, varInShadow);
    //     }
    //     Color varReflect = GetColorReflect(argIxState, argLimit);
    //     Color varRefract = GetColorRefracted(argIxState, argLimit);
    //     Color varRes = varDiffuse + varReflect + varRefract;
    //     if (argIxState._fieldObject._fieldMaterial._fieldReflective > 0 && argIxState._fieldObject._fieldMaterial._fieldTransparency > 0) {
    //         double varReflectance = argIxState.GetSchlick();
    //         varRes = varDiffuse + (varReflect * varReflectance) + (varRefract * (1-varReflectance));
    //     }
    //     return varRes;
    // }
    public Color GetColorShaded(IntersectionState argIxState, int argLimit = 5)
    {
        Color varDiffuse = new Color(0,0,0);
        for (int i = 0; i < _fieldLights.Count();++i)
        {
            bool varInShadow = CheckShadowed(_fieldLights[i], argIxState._fieldOverPoint);
            varDiffuse = varDiffuse + argIxState._fieldObject.GetColor(_fieldLights[i], argIxState._fieldOverPoint, argIxState._fieldPov, argIxState._fieldNormal, varInShadow);
        }
        Color varReflect = GetColorReflect(argIxState, argLimit);
        Color varRefract = GetColorRefracted(argIxState, argLimit);
        Color varRes = varDiffuse + varReflect + varRefract;
        if (argIxState._fieldObject._fieldMaterial._fieldReflective > 0 && argIxState._fieldObject._fieldMaterial._fieldTransparency > 0) {
            double varReflectance = argIxState.GetSchlick();
            varRes = varDiffuse + (varReflect * varReflectance) + (varRefract * (1-varReflectance));
        }
        return varRes;
    }
    public Color GetColorReflect(IntersectionState argIntersectionState, int argLimit = 5) {
        if (argLimit < 1) return new Color(0,0,0);
        Comparinator varComp = new Comparinator();
        if (varComp.CheckFloat(argIntersectionState._fieldObject._fieldMaterial._fieldReflective, 0)) return new Color(0,0,0);
        Ray varRayReflect = new Ray(argIntersectionState._fieldOverPoint, argIntersectionState._fieldReflect);
        Color varColor = GetColor(varRayReflect, argLimit - 1);
        Color varRes = varColor * argIntersectionState._fieldObject._fieldMaterial._fieldReflective;
        return varRes;
    }
    public Color GetColorRefracted(IntersectionState argIntersectionState, int argLimit = 5) {
        if (argLimit < 1) return new Color(0,0,0);
        Comparinator varComp = new Comparinator();
        if (varComp.CheckFloat(argIntersectionState._fieldObject._fieldMaterial._fieldTransparency, 0)) { return new Color(0,0,0); }
        double varNToN = argIntersectionState._fieldRefractiveIndexOne/argIntersectionState._fieldRefractiveIndexTwo;
        double varCosThetaI = argIntersectionState._fieldPov.GetDotProduct(argIntersectionState._fieldNormal);
        double varSinThetaTSquared = (varNToN*varNToN) * (1.0-(varCosThetaI*varCosThetaI));
        if (varSinThetaTSquared > 1) { return new Color(0,0,0); }
        double varCosThetaT = Math.Sqrt(1.0-varSinThetaTSquared);
        Vector varRefractDirection = (argIntersectionState._fieldNormal * ((varNToN * varCosThetaI) - varCosThetaT)) - (argIntersectionState._fieldPov * varNToN);
        Ray varRefractRay = new Ray(argIntersectionState._fieldUnderPoint, varRefractDirection);
        Color varRefractColor = GetColor(varRefractRay, argLimit-1) * argIntersectionState._fieldObject._fieldMaterial._fieldTransparency;
        return varRefractColor;
    }
    public bool CheckShadowed(PointSource argLight, Point argPoint) {
        bool varFlagShadow = false;
        Vector varDirection = argLight.mbrPosition - argPoint;
        double varDistance = varDirection.GetMagnitude();
         Vector varDirectionNormalized = varDirection.GetNormal();
        Ray varRay = new Ray (argPoint, varDirectionNormalized);
        Intersection varHit = GetIntersect(varRay).GetHit();
        bool varShadow = varHit._fieldExists && (varHit._fieldTime < varDistance);
        varFlagShadow = varShadow ? true : varFlagShadow;
        return varFlagShadow;
    }
    public void SetObject(Form argObject) {
        _fieldObjects.Add(argObject);
    }
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
    }
};

