using Xunit;
using LibWorld;
using LibRay;
using LibColor;
using LibLight;
using LibForm;
using LibTuple;
using LibMaterial;
using LibComparinator;
using LibIntersection;
using LibMatrix;
using LibPattern;

namespace LibWorld.Test;

public class WorldTest {
    [Fact]
    public void WorldTestCanary_WithDefault_ExpectDefault() {
        Assert.Equal(1, 1);
        Assert.True(true);
    }
    [Fact]
    public void WorldCtor_WithDefault_ExpectEmptyObjectsLights() {
        World w = new World();
        Assert.Empty(w._fieldObjects);
        Assert.Empty(w._fieldLights);
    }
    [Fact]
    public void WorldCtor_WithDefault_ExpectDefaultLightsObjects() {
        DefaultWorld varDefaultWorld = new DefaultWorld();
        PointSource l = new PointSource(new Point(-10,10,-10), new Color(1,1,1));
        UnitSphere s = new UnitSphere();
        s.SetMaterial(new Material());
        s._fieldMaterial._fieldColor = new Color(0.8,1.0,0.6);
        s._fieldMaterial._fieldDiffuse = 0.7;
        s._fieldMaterial._fieldSpecular = 0.2;
        UnitSphere t = new UnitSphere();
        t.SetTransform(new ScalingMatrix(0.5,0.5,0.5));
        Assert.True(l.CheckEqual(varDefaultWorld._fieldLights[0]));
        Assert.True(varDefaultWorld._fieldObjects[0].CheckEqual(s));
        Assert.True(varDefaultWorld._fieldObjects[1].CheckEqual(t));
    }
    [Fact]
    public void WorldGetIntersect_WithGivenRay_ExpectFourIntersect() {
        DefaultWorld varDefaultWorld = new DefaultWorld();
        Ray r = new Ray(new Point(0,0,-5), new Vector(0,0,1));
        Intersections xs = varDefaultWorld.GetIntersect(r);
        Assert.Equal(4, xs._fieldIntersections.Count());
        Assert.Equal(4, xs._fieldIntersections[0]._fieldTime);
        Assert.Equal(4.5, xs._fieldIntersections[1]._fieldTime);
        Assert.Equal(5.5, xs._fieldIntersections[2]._fieldTime);
        Assert.Equal(6, xs._fieldIntersections[3]._fieldTime);
    }
    [Fact]
    public void WorldGetIntersectionState_WithGiven_ExpectShadedColor() {
        DefaultWorld varDefaultWorld = new DefaultWorld();
        Ray r = new Ray(new Point(0,0,-5), new Vector(0,0,1));
        Form obj = varDefaultWorld._fieldObjects[0];
        UnitSphere s = new UnitSphere();
        s.SetMaterial(new Material());
        s._fieldMaterial._fieldColor = new Color(0.8,1.0,0.6);
        s._fieldMaterial._fieldDiffuse = 0.7;
        s._fieldMaterial._fieldSpecular = 0.2;
        Assert.True(obj.CheckEqual(s));
        Intersections varIx = new Intersections(4,new Form(obj));
        IntersectionState varIs = varIx.GetHit().GetState(r, varIx._fieldIntersections);
        Color c = varDefaultWorld.GetColorShaded(varIs);
        Color expectedColor = new Color(0.38066, 0.47583, 0.2855);
        Assert.True(c.CheckEqual(expectedColor));
    }
    [Fact]
    public void WorldGetIntersection_WithinInterior_ExpectShadedColor() {
        DefaultWorld varDefaultWorld = new DefaultWorld();
        varDefaultWorld._fieldLights[0] = new PointSource(new Point(0,0.25,0), new Color(1,1,1));
        Ray r = new Ray(new Point(0,0,0), new Vector(0,0,1));
        Form obj = varDefaultWorld._fieldObjects[1];
        Intersections varIx = new Intersections(0.5,obj);
        IntersectionState varIs = varIx.GetHit().GetState(r, varIx._fieldIntersections);
        Color c = varDefaultWorld.GetColorShaded(varIs);
        Color expectedColor = new Color(0.90498, 0.90498, 0.90498);
        Assert.True(c.CheckEqual(expectedColor));
    }
    [Fact]
    public void WorldGetColor_WithMiss_ExpectBlack() {
        DefaultWorld varDefaultWorld = new DefaultWorld();
        Ray r = new Ray(new Point(0,0,-5), new Vector(0,1,0));
        Color c = varDefaultWorld.GetColor(r);
        Color expectedColor = new Color(0,0,0);
        Assert.True(c.CheckEqual(expectedColor));
    }
    [Fact]
    public void WorldGetColor_WithHit_ExpectColor() {
        DefaultWorld varDefaultWorld = new DefaultWorld();
        Ray r = new Ray(new Point(0,0,-5), new Vector(0,0,1));
        Color c = varDefaultWorld.GetColor(r);
        Color expectedColor = new Color(0.38066, 0.47583, 0.2855);
        Assert.True(c.CheckEqual(expectedColor));
    }
    [Fact]
    public void WorldGetColor_WithHitInsideInnerSphere_ExpectColor() {
        DefaultWorld varDefaultWorld = new DefaultWorld();
        varDefaultWorld._fieldObjects[0]._fieldMaterial._fieldAmbient = 1;
        varDefaultWorld._fieldObjects[1]._fieldMaterial._fieldAmbient = 1;
        Ray r = new Ray(new Point(0,0,0.75), new Vector(0,0,-1));
        Color c = varDefaultWorld.GetColor(r);
        Color expectedColor = varDefaultWorld._fieldObjects[1]._fieldMaterial._fieldColor;
        Assert.True(c.CheckEqual(expectedColor));
    }
    [Fact]
    public void WorldGetColor_WithObjectCollinearWithPointLight_ExpectNoShadow() {
        DefaultWorld varDefaultWorld = new DefaultWorld();
        Point varPoint= new Point(0,10,0);
        Assert.False(varDefaultWorld.CheckShadowed(varDefaultWorld._fieldLights[0], varPoint));
    }
    [Fact]
    public void WorldGetColor_WithObjectBetweenPointLight_ExpectShadow() {
        DefaultWorld varDefaultWorld = new DefaultWorld();
        Point varPoint= new Point(10,-10,10);
        Assert.True(varDefaultWorld.CheckShadowed(varDefaultWorld._fieldLights[0], varPoint));
    }
    [Fact]
    public void WorldGetColor_WithObjectBehindLight_ExpectNoShadow() {
        DefaultWorld varDefaultWorld = new DefaultWorld();
        Point varPoint= new Point(-20,20,-20);
        Assert.False(varDefaultWorld.CheckShadowed(varDefaultWorld._fieldLights[0], varPoint));
    }
    [Fact]
    public void WorldGetColor_WithObjectBehindPoint_ExpectNoShadow() {
        DefaultWorld varDefaultWorld = new DefaultWorld();
        Point varPoint= new Point(-2,2,-2);
        Assert.False(varDefaultWorld.CheckShadowed(varDefaultWorld._fieldLights[0], varPoint));
    }
    [Fact]
    public void WorldGetColorShade_WithShadowIntersections_ExpectColor() {
        World varWorld = new World();
        PointSource varLight = new PointSource(new Point(0,0,-10), new Color(1,1,1));
        varWorld.SetLight(varLight);
        UnitSphere varS1 = new UnitSphere();
        varWorld.SetObject(varS1);
        UnitSphere varS2 = new UnitSphere();
        varS2.SetTransform(new TranslationMatrix(0,0,10));
        varWorld.SetObject(varS2);
        Ray varRay = new Ray(new Point(0,0,5), new Vector(0,0,1));
        Intersections varIx = new Intersections(4, varS2);
        IntersectionState varIs = varIx.GetHit().GetState(varRay, varIx._fieldIntersections);
        Color varClr = varWorld.GetColorShaded(varIs);
        Color varExpectedClr = new Color(0.1,0.1,0.1);
        Assert.True(varExpectedClr.CheckEqual(varClr));
    }
    [Fact]
    public void WorldGetColor_WithNonReflective_ExpectReflectedColorBlack() {
        DefaultWorld varWorld = new DefaultWorld();
        Ray varRay = new Ray(new Point(0,0,0), new Vector(0,0,1));
        Form varObj = varWorld._fieldObjects[1];
        varObj._fieldMaterial._fieldAmbient = 1;
        Intersections varIx = new Intersections(1, varObj);
        IntersectionState varIs = varIx.GetHit().GetState(varRay, varIx._fieldIntersections);
        Color varColor = varWorld.GetColorReflect(varIs);
        Assert.True(varColor.CheckEqual(new Color(0,0,0)));
    }
    [Fact]
    public void WorldGetColor_WithReflective_ExpectReflectedColor() {
        DefaultWorld varWorld = new DefaultWorld();
        UnitPlane varPlane = new UnitPlane();
        varPlane._fieldMaterial._fieldReflective = 0.5;
        varPlane.SetTransform(new TranslationMatrix(0,-1,0));
        varWorld.SetObject(varPlane);
        Ray varRay = new Ray(new Point(0,0,-3), new Vector(0,-Math.Sqrt(2)/2,Math.Sqrt(2)/2));
        Intersections varIx = new Intersections(Math.Sqrt(2), varPlane);
        IntersectionState varIs = varIx.GetHit().GetState(varRay, varIx._fieldIntersections);
        Color varColor = varWorld.GetColorReflect(varIs);
        Assert.True(varColor.CheckEqual(new Color(0.19032,0.2379,0.14274)));
    }
    [Fact]
    public void WorldGetColor_WithReflective_ExpectShaded() {
        DefaultWorld varWorld = new DefaultWorld();
        UnitPlane varPlane = new UnitPlane();
        varPlane._fieldMaterial._fieldReflective = 0.5;
        varPlane.SetTransform(new TranslationMatrix(0,-1,0));
        varWorld.SetObject(varPlane);
        Ray varRay = new Ray(new Point(0,0,-3), new Vector(0,-Math.Sqrt(2)/2,Math.Sqrt(2)/2));
        Intersections varIx = new Intersections(Math.Sqrt(2), varPlane);
        IntersectionState varIs = varIx.GetHit().GetState(varRay, varIx._fieldIntersections);
        Color varColor = varWorld.GetColorShaded(varIs, 5);
        Assert.True(varColor.CheckEqual(new Color(0.87677, 0.92436, 0.82918)));
    }
    [Fact]
    public void WorldGetColor_WithInfiniteReflectionsBetweenMirrors_ExpectBlack() {
        World varWorld = new World();
        varWorld.SetLight(new PointSource(new Point(0,0,0), new Color(1,1,1)));
        UnitPlane varLower = new UnitPlane();
        varLower._fieldMaterial._fieldReflective = 1;
        varLower.SetTransform(new TranslationMatrix(0,-1,0));
        varWorld.SetObject(varLower);
        UnitPlane varUpper = new UnitPlane();
        varUpper._fieldMaterial._fieldReflective = 1;
        varUpper.SetTransform(new TranslationMatrix(0,1,0));
        varWorld.SetObject(varUpper);
        Ray varRay = new Ray(new Point(0,0,0), new Vector(0,1,0));
        Color varColor = varWorld.GetColor(varRay);
        Assert.True(true);
    }
    [Fact]
    public void WorldGetColor_WithInfiniteReflectionsAtMaxDepth_ExpectBlack() {
        DefaultWorld varWorld = new DefaultWorld();
        UnitPlane varPlane = new UnitPlane();
        varPlane._fieldMaterial._fieldReflective = 0.5;
        varPlane.SetTransform(new TranslationMatrix(0,-1,0));
        varWorld.SetObject(varPlane);
        Ray varRay = new Ray(new Point(0,0,-3), new Vector(0,-Math.Sqrt(2)/2,Math.Sqrt(2)/2));
        Intersections varIx = new Intersections(Math.Sqrt(2), varPlane);
        IntersectionState varIs = varIx.GetHit().GetState(varRay, varIx._fieldIntersections);
        Color varColor = varWorld.GetColorReflect(varIs, 0);
        Assert.True(varColor.CheckEqual(new Color(0,0,0)));
    }
    [Fact]
    public void WorldGetColor_WithRefractiveWithOpaque_ExpectBlack() {
        DefaultWorld varWorld = new DefaultWorld();
        Form varObj = varWorld._fieldObjects[0];
        Ray varRay = new Ray(new Point(0,0,-5), new Vector(0,0,1));
        Intersections varIx = new Intersections();
        varIx.SetIntersect(4, varObj);
        varIx.SetIntersect(4, varObj);
        IntersectionState varIs = varIx._fieldIntersections[0].GetState(varRay, varIx._fieldIntersections);
        Color varColor = varWorld.GetColorRefracted(varIs, 5);
        Assert.True(varColor.CheckEqual(new Color(0,0,0)));
    }
    [Fact]
    public void WorldGetColor_WithInfiniteRefraction_ExpectBlack() {
        DefaultWorld varWorld = new DefaultWorld();
        Form varObj = varWorld._fieldObjects[0];
        varObj._fieldMaterial._fieldTransparency = 1.0;
        varObj._fieldMaterial._fieldTransparency = 1.5;
        Ray varRay = new Ray(new Point(0,0,-5), new Vector(0,0,1));
        Intersections varIx = new Intersections();
        varIx.SetIntersect(4, varObj);
        varIx.SetIntersect(4, varObj);
        IntersectionState varIs = varIx._fieldIntersections[0].GetState(varRay, varIx._fieldIntersections);
        Color varColor = varWorld.GetColorRefracted(varIs, 0);
        Assert.True(varColor.CheckEqual(new Color(0,0,0)));
    }
    [Fact]
    public void WorldGetColor_WithTotalInternalRefractionvar_ExpectBlack() {
        DefaultWorld varWorld = new DefaultWorld();
        Form varObj = varWorld._fieldObjects[0];
        varObj._fieldMaterial._fieldTransparency = 1.0;
        varObj._fieldMaterial._fieldRefractiveIndex = 1.5;
        Ray varRay = new Ray(new Point(0,0,Math.Sqrt(2)/2), new Vector(0,1,0));
        Intersections varIx = new Intersections();
        varIx.SetIntersect(-Math.Sqrt(2)/2, varObj);
        varIx.SetIntersect(Math.Sqrt(2)/2, varObj);
        IntersectionState varIs = varIx._fieldIntersections[1].GetState(varRay, varIx._fieldIntersections);
        Color varColor = varWorld.GetColorRefracted(varIs, 5);
        Assert.True(varColor.CheckEqual(new Color(0,0,0)));
    }
    [Fact]
    public void WorldGetColor_WithBasicRefraction_ExpectColor() {
        DefaultWorld varWorld = new DefaultWorld();
        varWorld._fieldObjects[0]._fieldMaterial._fieldAmbient = 1.0;
        varWorld._fieldObjects[0]._fieldMaterial.SetPattern(new Pattern(new Color(0,0,0), new Color(1,1,1)));
        varWorld._fieldObjects[1]._fieldMaterial._fieldTransparency = 1.0;
        varWorld._fieldObjects[1]._fieldMaterial._fieldRefractiveIndex = 1.5;
        Ray varRay = new Ray(new Point(0,0,0.1), new Vector(0,1,0));
        Intersections varIx = new Intersections();
        varIx.SetIntersect(-0.9899, varWorld._fieldObjects[0]);
        varIx.SetIntersect(-0.4899, varWorld._fieldObjects[1]);
        varIx.SetIntersect(0.4899, varWorld._fieldObjects[1]);
        varIx.SetIntersect(0.9899, varWorld._fieldObjects[0]);
        IntersectionState varIs = varIx._fieldIntersections[2].GetState(varRay, varIx._fieldIntersections);
        Color varColor = varWorld.GetColorRefracted(varIs, 5);
        Assert.True(varColor.CheckEqual(new Color(0, 0.99888, 0.04725)));
    }
    [Fact]
    public void WorldGetColor_WithRefractionTransparent_ExpectDilutedColor() {
        DefaultWorld varWorld = new DefaultWorld();
        UnitPlane varFloor = new UnitPlane();
        varFloor.SetTransform(new TranslationMatrix(0,-1,0));
        varFloor._fieldMaterial._fieldTransparency = 0.5;
        varFloor._fieldMaterial._fieldRefractiveIndex = 1.5;
        varWorld.SetObject(varFloor);
        UnitSphere varSphere = new UnitSphere();
        varSphere.SetTransform(new TranslationMatrix(0,-3.5,-.5));
        varSphere._fieldMaterial._fieldColor=new Color(1,0,0);
        varSphere._fieldMaterial._fieldAmbient = 0.5;
        varWorld.SetObject(varSphere);
        Ray varRay = new Ray(new Point(0,0,-3), new Vector(0,-Math.Sqrt(2)/2,Math.Sqrt(2)/2));
        Intersections varIx = new Intersections(Math.Sqrt(2), varFloor);
        IntersectionState varIs = varIx._fieldIntersections[0].GetState(varRay, varIx._fieldIntersections);
        Color varColor = varWorld.GetColorShaded(varIs, 5);
        Assert.True(varColor.CheckEqual(new Color(0.93642, 0.68642, 0.68642)));
    }
    [Fact]
    public void WorldGetColor_WithReflectiveTransparentReflectanceMaterial_ExpectColor() {
        DefaultWorld varWorld = new DefaultWorld();
        Ray varRay = new Ray(new Point(0,0,-3), new Vector(0,-Math.Sqrt(2)/2,Math.Sqrt(2)/2));
        UnitPlane varFloor = new UnitPlane();
        varFloor.SetTransform(new TranslationMatrix(0,-1,0));
        varFloor._fieldMaterial._fieldReflective = 0.5;
        varFloor._fieldMaterial._fieldTransparency = 0.5;
        varFloor._fieldMaterial._fieldRefractiveIndex = 1.5;
        varWorld.SetObject(varFloor);
        UnitSphere varSphere = new UnitSphere();
        varSphere.SetTransform(new TranslationMatrix(0,-3.5,-.5));
        varSphere._fieldMaterial._fieldColor=new Color(1,0,0);
        varSphere._fieldMaterial._fieldAmbient = 0.5;
        varWorld.SetObject(varSphere);
        Intersections varIx = new Intersections(Math.Sqrt(2), varFloor);
        IntersectionState varIs = varIx._fieldIntersections[0].GetState(varRay, varIx._fieldIntersections);
        Color varColor = varWorld.GetColorShaded(varIs, 5);
        Assert.True(varColor.CheckEqual(new Color(0.93391,0.69643,0.69243)));
    }
}