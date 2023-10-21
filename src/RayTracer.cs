using LibComparinator;
using LibTuple;
using LibCanvas;
using LibColor;
using LibMaterial;
using LibForm;
using LibIntersection;
using LibRay;
using LibLight;
using LibPattern;
using LibProjectMeta;
using LibMatrix;
using LibCamera;
using LibWorld;
using LibParser;
namespace LibRayTracer;

public class Projectile {
	public SpaceTuple position;
	public SpaceTuple velocity;
	public Projectile(SpaceTuple pos, SpaceTuple vel)
	{
		position = pos;
		velocity = vel;
	}
}
public class Environment {
	public Vector gravity;
	public Vector wind;
	public Environment(Vector g, Vector w)
	{
		gravity = g;
		wind = w;
	}
};

public class Simulation {
    public Simulation () {
        _fieldPM = new ProjectMeta();
        c = new Canvas(_fieldPM.GetPPMWidth(), _fieldPM.GetPPMHeight());
    }
	public Projectile tick(Environment env, Projectile ball)
	{
		SpaceTuple position = ball.position + ball.velocity;
		SpaceTuple velocity = ball.velocity + env.gravity + env.wind;
		return new Projectile(position, velocity);
	}
	public void Fire(int power) {
		c.SetFill(new Color(1, 1, 1));
		Projectile b = new Projectile(new Point(0, 1, 0), (new Vector(1, 1, 0).GetNormal()) * power);
		Environment e = new Environment(new Vector(0, -.1, 0), new Vector(-.01, 0, 0));
		while (b.position._fieldY >= 0)
		{
			b = tick(e,b);
			Color white = new Color(0, 0, 0);
			c.SetPixel((int)b.position._fieldX, (int)(_fieldPM.GetPPMHeight() - b.position._fieldY), white);
		}
		c.RenderFile();
	}
	private Canvas c;
    private ProjectMeta _fieldPM;
}

public class RayTracer {
    private ProjectMeta _fieldPM;
	public RayTracer() {
		_fieldPM = new ProjectMeta();
	}
    public void Test() {
        Point varTestEye = new Point(0,0,-5);
        int varTestCanvasWidthSingle = 100;
        int varTestCanvasHeightSingle = 100;
        double varTestCanvasAngleSingle = _fieldPM.GetPI()/4;

        PointSource varTestLight = new PointSource(new Point(-10,10,-10), new Color(1,1,1));

        Color varWhite = new Color(1,1,1);
        Color varOrange = new Color(.8, 0.333, 0);
        Material varTestMat = new Material();
        varTestMat._fieldColor = varOrange;
        // varTestMat._fieldColor = varWhite;
        varTestMat._fieldDiffuse = 0.7;
        varTestMat._fieldSpecular = 0.3;
        Material varTestMatStripe = new Material();
        varTestMatStripe.SetPattern(new PatternStripe(varOrange, varWhite));
        Material varTestMatGradient = new Material();
        varTestMatGradient.SetPattern(new PatternGradient(varOrange, varWhite));
        //varTestMatGradient._fieldPattern.SetTransform(new ScalingMatrix(varTestCanvasWidthSingle,0.01,varTestCanvasHeightSingle));
        Material varTestMatChecker = new Material();
        varTestMatChecker.SetPattern(new PatternChecker(varOrange, varWhite));
        //varTestMatChecker._fieldPattern.SetTransform(new ScalingMatrix(0.05,0.05,0.05));
        Material varTestMatRing = new Material();
        varTestMatRing.SetPattern(new PatternRing(varOrange, varWhite));
        //varTestMatRing._fieldPattern.SetTransform(new ScalingMatrix(0.05,0.01,0.05));

        // DirectTracerShadow("Circle", varTestEye, 10,10, 100, new Color(1,1,1));
        // DirectTracerShadow("OrangeCircle", varTestEye, 10,10, 100, varOrange);
        // DirectRenderSphere("OrangeSphere", varTestEye, varTestLight, 10, 10, 100, varOrange);
        
        // CameraRenderSphere("OrangeSphere", new ViewMatrix(varTestEye, new Point(0,0,0), new Vector(0,1,0)), varTestCanvasWidthSingle, varTestCanvasHeightSingle, varTestCanvasAngleSingle, varTestLight, new ScalingMatrix(1.5,1.5,1.5), varTestMat);
        // CameraRenderSphere("OrangeSphereStripe", new ViewMatrix(varTestEye, new Point(0,0,0), new Vector(0,1,0)), varTestCanvasWidthSingle, varTestCanvasHeightSingle, varTestCanvasAngleSingle, varTestLight, new ScalingMatrix(1.5,1.5,1.5), varTestMatStripe);
        // CameraRenderSphere("OrangeSphereGradient", new ViewMatrix(varTestEye, new Point(0,0,0), new Vector(0,1,0)), varTestCanvasWidthSingle, varTestCanvasHeightSingle, varTestCanvasAngleSingle, varTestLight, new ScalingMatrix(1.5,1.5,1.5), varTestMatGradient);
        // CameraRenderSphere("OrangeSphereCheckered", new ViewMatrix(varTestEye, new Point(0,0,0), new Vector(0,1,0)), varTestCanvasWidthSingle, varTestCanvasHeightSingle, varTestCanvasAngleSingle, varTestLight, new ScalingMatrix(1.5,1.5,1.5), varTestMatChecker);
        // CameraRenderSphere("OrangeSphereRing", new ViewMatrix(varTestEye, new Point(0,0,0), new Vector(0,1,0)), varTestCanvasWidthSingle, varTestCanvasHeightSingle, varTestCanvasAngleSingle, varTestLight, new ScalingMatrix(1.5,1.5,1.5), varTestMatRing);

        // CameraRenderPlane("OrangePlane", new ViewMatrix(varTestEye, new Point(0,0,0), new Vector(0,1,0)), varTestCanvasWidthSingle, varTestCanvasHeightSingle, varTestCanvasAngleSingle, varTestLight, new XRotationMatrix(_fieldPM.GetPI()/2), varTestMat);
        // CameraRenderPlane("OrangePlaneStripe", new ViewMatrix(varTestEye, new Point(0,0,0), new Vector(0,1,0)), varTestCanvasWidthSingle, varTestCanvasHeightSingle, varTestCanvasAngleSingle, varTestLight, new XRotationMatrix(_fieldPM.GetPI()/2), varTestMatStripe);
        // CameraRenderPlane("OrangePlaneGradient", new ViewMatrix(varTestEye, new Point(0,0,0), new Vector(0,1,0)), varTestCanvasWidthSingle, varTestCanvasHeightSingle, varTestCanvasAngleSingle, varTestLight, new XRotationMatrix(_fieldPM.GetPI()/2), varTestMatGradient);
        // CameraRenderPlane("OrangePlaneCheckered", new ViewMatrix(varTestEye, new Point(0,0,0), new Vector(0,1,0)), varTestCanvasWidthSingle, varTestCanvasHeightSingle, varTestCanvasAngleSingle, varTestLight, new XRotationMatrix(_fieldPM.GetPI()/2), varTestMatChecker);
        // CameraRenderPlane("OrangePlaneCheckerTilt", new ViewMatrix(varTestEye, new Point(0,0,0), new Vector(0,1,0)), varTestCanvasWidthSingle, varTestCanvasHeightSingle, varTestCanvasAngleSingle, varTestLight, new XRotationMatrix(-_fieldPM.GetPI()/4), varTestMatChecker);
        // CameraRenderPlane("OrangePlaneRing", new ViewMatrix(varTestEye, new Point(0,0,0), new Vector(0,1,0)), varTestCanvasWidthSingle, varTestCanvasHeightSingle, varTestCanvasAngleSingle, varTestLight, new XRotationMatrix(_fieldPM.GetPI()/2), varTestMatRing);

        // ExampleSphereMirror();
        // ExampleSphereSphere("Checker", new PatternChecker(new Color(0.15, 0.15, 0.15), new Color(0.85, 0.85, 0.85)));
        // ExampleSphereSphere("Gradient", new PatternGradient(new Color(0.15, 0.15, 0.15), new Color(0.85, 0.85, 0.85)));
        // ExampleSphereSphere("Ring", new PatternRing(new Color(0.15, 0.15, 0.15), new Color(0.85, 0.85, 0.85)));
        // ExampleMirrorRoom();
        // ExampleCubesRoom();
        // ExampleCylinders();
        // ExampleCompositeHexagon();

        // RenderOBJOctahedron();
        // RenderOBJPyramid();
        // RenderOBJHumanoidTri();
        // RenderOBJHumanoidQuad();
        // RenderOBJTeapot();
        // RenderOBJCow();
        RenderOBJTeddy();
//         // cameraRenderSpheres();
//         // cameraRenderPlanes();

//         // cameraRenderSpherePlane();
//         // cameraRenderPatterns();
//         // cameraRenderReflections();
//         // cameraRenderReflectRefract();
    }

    public void RenderOBJTeapot() {
        World varEnv = new World();
        varEnv.SetLight(new PointSource(new Point(0,120,0), new Color(1,1,1)));
        String varData = File.ReadAllText("../data/utah-teapot.obj");
        ParserWaveFrontObj varParser = new ParserWaveFrontObj();
        varParser.ParseWaveFrontObj(varData);
        CompositeGroup varObj = varParser.GetGroup();
        // varObj.SetTransform(new XRotationMatrix(-_fieldPM.GetPI()/2));
        // varObj.SetTransform(new XRotationMatrix(-_fieldPM.GetPI()/2));
        varObj.SetTransform(new TranslationMatrix(-1,0,0));
        varEnv.SetObject(varObj);
        Camera varCamera = new Camera(100,100,1.152);
        varCamera.SetTransform(new ViewMatrix(new Point(10,0,0), new Point(0,0,0), new Vector(0,1,0)));
        Canvas img = varCamera.RenderCanvas(varEnv);
        img.RenderFile("teapot");        
    }
    public void RenderOBJCow() {
        World varEnv = new World();
        varEnv.SetLight(new PointSource(new Point(0,120,0), new Color(1,1,1)));
        String varData = File.ReadAllText("../data/cow-nonormals.obj");
        ParserWaveFrontObj varParser = new ParserWaveFrontObj();
        varParser.ParseWaveFrontObj(varData);
        CompositeGroup varObj = varParser.GetGroup();
        varObj.SetTransform(new YRotationMatrix(-_fieldPM.GetPI()/4).GetRotateX(-_fieldPM.GetPI()/2));
        varEnv.SetObject(varObj);
        Camera varCamera = new Camera(100,100,1.152);
        varCamera.SetTransform(new ViewMatrix(new Point(0,-1,0), new Point(0,0,0), new Vector(0,1,0)));
        Canvas img = varCamera.RenderCanvas(varEnv);
        img.RenderFile("cow");        
    }
    public void RenderOBJTeddy() {
        World varEnv = new World();
        varEnv.SetLight(new PointSource(new Point(20,20,20), new Color(1,1,1)));
        String varData = File.ReadAllText("../data/teddy.obj");
        ParserWaveFrontObj varParser = new ParserWaveFrontObj();
        varParser.ParseWaveFrontObj(varData);
        CompositeGroup varObj = varParser.GetGroup();
        varEnv.SetObject(varObj);
        Camera varCamera = new Camera(200,200,1.152);
        varCamera.SetTransform(new ViewMatrix(new Point(40,20,40), new Point(0,0,0), new Vector(0,1,0)));
        Canvas img = varCamera.RenderCanvas(varEnv);
        img.RenderFile("teddy");        
    }
    public void RenderOBJOctahedron() {
        World varEnv = new World();
        PointSource varLight = new PointSource(new Point(0,5,0), new Color(1,1,1));
        varEnv.SetLight(varLight);
        String varData = File.ReadAllText("../data/octahedron.obj");
        ParserWaveFrontObj varParser = new ParserWaveFrontObj();
        varParser.ParseWaveFrontObj(varData);
        varEnv.SetObject(varParser.GetGroup());
        Camera varCamera = new Camera(200,100,1.152);
        varCamera.SetTransform(new ViewMatrix(new Point(0,2.5,-2.5), new Point(0,0,0), new Vector(0,1,0)));
        Canvas img = varCamera.RenderCanvas(varEnv);
        img.RenderFile("octahedron");        
    }
    public void RenderOBJPyramid() {
        World varEnv = new World();
        PointSource varLight = new PointSource(new Point(0,5,0), new Color(1,1,1));
        varEnv.SetLight(varLight);
        String varData = File.ReadAllText("../data/pyramid.obj");
        ParserWaveFrontObj varParser = new ParserWaveFrontObj();
        varParser.ParseWaveFrontObj(varData);
        varEnv.SetObject(varParser.GetGroup());
        Camera varCamera = new Camera(200,100,1.152);
        varCamera.SetTransform(new ViewMatrix(new Point(0,5,-5), new Point(0,0,0), new Vector(0,1,0)));
        Canvas img = varCamera.RenderCanvas(varEnv);
        img.RenderFile("pyramid");
    }
    public void RenderOBJHumanoidTri() {
        World varEnv = new World();
        PointSource varLight = new PointSource(new Point(0,30,0), new Color(1,1,1));
        varEnv.SetLight(varLight);
        String varData = File.ReadAllText("../data/humanoid_tri.obj");
        ParserWaveFrontObj varParser = new ParserWaveFrontObj();
        varParser.ParseWaveFrontObj(varData);
        varEnv.SetObject(varParser.GetGroup());
        Camera varCamera = new Camera(200,100,1.152);
        varCamera.SetTransform(new ViewMatrix(new Point(30,30,-30), new Point(0,0,0), new Vector(0,1,0)));
        Canvas img = varCamera.RenderCanvas(varEnv);
        img.RenderFile("humanoid_tri");        
    }
    public void RenderOBJHumanoidQuad() {
        World varEnv = new World();
        PointSource varLight = new PointSource(new Point(0,10,0), new Color(1,1,1));
        varEnv.SetLight(varLight);
        String varData = File.ReadAllText("../data/humanoid_quad.obj");
        ParserWaveFrontObj varParser = new ParserWaveFrontObj();
        varParser.ParseWaveFrontObj(varData);
        varEnv.SetObject(varParser.GetGroup());
        Camera varCamera = new Camera(200,100,1.152);
        varCamera.SetTransform(new ViewMatrix(new Point(0,0,-10), new Point(0,0,0), new Vector(0,1,0)));
        Canvas img = varCamera.RenderCanvas(varEnv);
        img.RenderFile("humanoid_quad");        
    }

    public void DirectTracerShadow(String argSpecs, Point argPov, int argScreenWidth, double argScreenZ, int argScreenPixels, Color argColor)
    {
        double wallXY = (double)argScreenWidth/2;
        double pixelSize = (double)argScreenWidth/argScreenPixels;
        Canvas canvas = new Canvas(argScreenPixels,argScreenPixels);

        UnitSphere obj = new UnitSphere();

        for (int i = 0; i <= argScreenPixels; ++i)
        {
            double worldY = wallXY - pixelSize * i;
            for (int j = 0; j <= argScreenPixels; ++j)
            {
                double worldX = -wallXY + pixelSize * j;
                Point position = new Point(worldX, worldY, argScreenZ);
                Ray r = new Ray(argPov, (position-argPov).GetNormal());
                List<Intersection> xs = obj.GetIntersections(r)._fieldIntersections;
                if (xs.Count() != 0)
                {
                    canvas.SetPixel(i,j,argColor);
                }
            }
        }
        canvas.RenderFile(argSpecs);
    }
    void DirectRenderSphere(String argSpecs, SpaceTuple argPov, PointSource argLight, int argScreenWidth, double argScreenZ, int argScreenPixels, Color argSphereColor)
    {
        Canvas varCanvas = new Canvas(argScreenPixels,argScreenPixels);
        double varScreenXY = (double)argScreenWidth/2;
        double varPixelSize = (double)argScreenWidth/argScreenPixels;

        UnitSphere varObj = new UnitSphere();
        varObj.SetMaterial(new Material());
        varObj._fieldMaterial._fieldColor = argSphereColor;

        for (int i = 0; i <= argScreenPixels; ++i)
        {
            double varWorldY = varScreenXY - varPixelSize * i;
            for (int j = 0; j <= argScreenPixels; ++j)
            {
                double varWorldX = -varScreenXY + varPixelSize * j;
                SpaceTuple varWorldPosition = new Point(varWorldX, varWorldY, argScreenZ);
                Ray r = new Ray(argPov, (varWorldPosition-argPov).GetNormal());
                List<Intersection> xs = varObj.GetIntersections(r)._fieldIntersections;
                if (xs.Count() != 0)
                {
                    SpaceTuple p = r.GetPosition(xs[0]._fieldTime);
                    SpaceTuple normal = xs[0]._fieldObject.GetNormal(p);
                    SpaceTuple pov = -r._fieldDirection;
                    Color shade = varObj._fieldMaterial.GetColor(varObj, argLight, p, pov, normal, false);
                    varCanvas.SetPixel(i,j,shade);
                }
            }
        }
        varCanvas.RenderFile(argSpecs);
    }
    void CameraRenderSphere(String argSpecs, ViewMatrix argView, int argScreenHeight, int argScreenWidth, double argFov, PointSource argLight, Matrix argSphereTransform, Material argSphereMaterial)
    {
        Camera varCamera = new Camera(argScreenWidth,argScreenHeight,argFov);
        varCamera.SetTransform(argView);
        World varEnv = new World();
        varEnv.SetLight(argLight);
        UnitSphere varObj = new UnitSphere();
        varObj.SetTransform(argSphereTransform);
        varObj.SetMaterial(argSphereMaterial);
        varEnv.SetObject(varObj);
        Canvas img = varCamera.RenderCanvas(varEnv);
        img.RenderFile(argSpecs);
    }
    void CameraRenderPlane(String argSpecs, ViewMatrix argView, int argScreenHeight, int argScreenWidth, double argFov, PointSource argLight, Matrix argObjTransform, Material argObjMaterial)
    {
        Camera varCamera = new Camera(argScreenWidth,argScreenHeight,argFov);
        varCamera.SetTransform(argView);
        World varEnv = new World();
        varEnv.SetLight(argLight);
        UnitPlane varObj = new UnitPlane();
        varObj.SetTransform(argObjTransform);
        varObj.SetMaterial(argObjMaterial);
        varEnv.SetObject(varObj);
        Canvas img = varCamera.RenderCanvas(varEnv);
        img.RenderFile(argSpecs);
    }
    void ExampleSphereMirror() {
        Camera varCamera = new Camera(200,200,0.5);
        varCamera.SetTransform(new ViewMatrix(new Point(-4.5, 0.85, -4), new Point(0, 0.85, 0), new Vector(0,1,0)));

        World varEnv = new World();

        PointSource varLight = new PointSource(new Point(-4.9, 4.9, 1), new Color(1, 1, 1));
        varEnv.SetLight(varLight);

        Material varWallMaterial = new Material();
        varWallMaterial.SetPattern(new PatternChecker(new Color(0, 0, 0), new Color(0.75,0.75,0.75)));
        varWallMaterial._fieldPattern.SetTransform(new ScalingMatrix(0.5, 0.5, 0.5));
        varWallMaterial._fieldSpecular = 0;
        
        UnitPlane varFloor = new UnitPlane();
        varFloor.SetTransform(new YRotationMatrix(0.31415));
        varFloor.SetMaterial(new Material());
        varFloor._fieldMaterial.SetPattern(new PatternChecker(new Color(0, 0, 0), new Color(0.75,0.75,0.75)));
        varFloor._fieldMaterial._fieldAmbient = 0.5;
        varFloor._fieldMaterial._fieldDiffuse = 0.4;
        varFloor._fieldMaterial._fieldSpecular = 0.8;
        varFloor._fieldMaterial._fieldReflective = 0.1;
        varEnv.SetObject(varFloor);

        UnitPlane varCeiling = new UnitPlane();
        varCeiling.SetTransform(new TranslationMatrix(0,5,0));
        varCeiling.SetMaterial(new Material());
        varCeiling._fieldMaterial.SetPattern(new PatternChecker(new Color(0.85, 0.85, 0.85), new Color(1, 1, 1)));
        varCeiling._fieldMaterial._fieldAmbient = 0.5;
        varCeiling._fieldMaterial._fieldSpecular = 0;
        varEnv.SetObject(varCeiling);

        UnitPlane varWallWest = new UnitPlane();
        varWallWest.SetTransform(new TranslationMatrix(-5,0,0).GetRotateZ(1.5708).GetRotateY(1.5708));
        varWallWest.SetMaterial(varWallMaterial);
        varEnv.SetObject(varWallWest);

        UnitPlane varWallEast = new UnitPlane();
        varWallEast.SetTransform(new TranslationMatrix(5,0,0).GetRotateZ(1.5708).GetRotateY(1.5708));
        varWallEast.SetMaterial(varWallMaterial);
        varEnv.SetObject(varWallEast);

        UnitPlane varWallNorth = new UnitPlane();
        varWallNorth.SetTransform(new TranslationMatrix(0,0,5).GetRotateX(1.5708));
        varWallNorth.SetMaterial(varWallMaterial);
        varEnv.SetObject(varWallNorth);

        UnitPlane varWallSouth = new UnitPlane();
        varWallSouth.SetTransform(new TranslationMatrix(0,0,-5).GetRotateX(1.5708));
        varWallSouth.SetMaterial(varWallMaterial);
        varEnv.SetObject(varWallSouth);

        UnitSphere varObjBg1 = new UnitSphere();
        varObjBg1.SetTransform(new TranslationMatrix(4,1,4));
        varObjBg1.SetMaterial(new Material());
        varObjBg1._fieldMaterial._fieldColor = new Color(.8,.1,.3);
        varObjBg1._fieldMaterial._fieldSpecular = 0;
        varEnv.SetObject(varObjBg1);

        UnitSphere varObjBg2 = new UnitSphere();
        varObjBg2.SetTransform(new TranslationMatrix(4.6,0.4,2.9).GetScale(.4,.4,.4));
        varObjBg2.SetMaterial(new Material());
        varObjBg2._fieldMaterial._fieldColor = new Color(.1,.8,.2);
        varObjBg2._fieldMaterial._fieldShininess = 200;
        varEnv.SetObject(varObjBg2);

        UnitSphere varObjBg3 = new UnitSphere();
        varObjBg3.SetTransform(new TranslationMatrix(2.6,0.6,4.4).GetScale(.6,.6,.6));
        varObjBg3.SetMaterial(new Material());
        varObjBg3._fieldMaterial._fieldColor = new Color(.2,.1,.8);
        varObjBg3._fieldMaterial._fieldShininess = 10;
        varObjBg3._fieldMaterial._fieldSpecular = 0.4;
        varEnv.SetObject(varObjBg3);

        UnitSphere varObjFg1 = new UnitSphere();
        varObjFg1.SetTransform(new TranslationMatrix(.25,1,0).GetScale(1,1,1));
        varObjFg1.SetMaterial(new Material());
        varObjFg1._fieldMaterial._fieldColor = new Color(.8,.8,.9);
        varObjFg1._fieldMaterial._fieldAmbient = 0;
        varObjFg1._fieldMaterial._fieldDiffuse = 0.2;
        varObjFg1._fieldMaterial._fieldSpecular = 0.9;
        varObjFg1._fieldMaterial._fieldShininess = 300;
        varObjFg1._fieldMaterial._fieldTransparency = 0.8;
        varObjFg1._fieldMaterial._fieldRefractiveIndex = 1.57;
        varEnv.SetObject(varObjFg1);

        Canvas img = varCamera.RenderCanvas(varEnv);
        img.RenderFile("RayTracerChallenge__Chapter11__Refraction");
    }
    void ExampleSphereSphere(String argSpecs, Pattern argBackground)
    {
        Camera varCamera = new Camera(300,300,0.45);
        varCamera.SetTransform(new ViewMatrix(new Point(0, 0, -5), new Point(0, 0, 0), new Vector(0,1,0)));

        World varEnv = new World();

        PointSource varLight = new PointSource(new Point(2, 10, -5), new Color(0.9, 0.9, 0.9));
        varEnv.SetLight(varLight);

        UnitPlane varWall = new UnitPlane();
        varWall.SetTransform(new TranslationMatrix(0,0,10).GetRotateX(1.5708));
        varWall.SetMaterial(new Material());
        varWall._fieldMaterial.SetPattern(argBackground);
        varWall._fieldMaterial._fieldAmbient = 0.8;
        varWall._fieldMaterial._fieldDiffuse = 0.2;
        varWall._fieldMaterial._fieldSpecular = 0;
        varEnv.SetObject(varWall);

        UnitSphere varObjBg1 = new UnitSphere();
        varObjBg1.SetMaterial(new Material());
        varObjBg1._fieldMaterial._fieldColor = new Color(1,1,1);
        varObjBg1._fieldMaterial._fieldAmbient = 0;
        varObjBg1._fieldMaterial._fieldDiffuse = 0;
        varObjBg1._fieldMaterial._fieldSpecular = 0.9;
        varObjBg1._fieldMaterial._fieldShininess = 300;
        varObjBg1._fieldMaterial._fieldReflective = 0.9;
        varObjBg1._fieldMaterial._fieldTransparency = 0.9;
        varObjBg1._fieldMaterial._fieldRefractiveIndex = 1.5;
        varEnv.SetObject(varObjBg1);

        UnitSphere varObjBg2 = new UnitSphere();
        varObjBg2.SetTransform(new ScalingMatrix(.5,.5,.5));
        varObjBg2.SetMaterial(new Material());
        varObjBg2._fieldMaterial._fieldColor = new Color(1,1,1);
        varObjBg2._fieldMaterial._fieldAmbient = 0;
        varObjBg2._fieldMaterial._fieldDiffuse = 0;
        varObjBg2._fieldMaterial._fieldSpecular = 0.9;
        varObjBg2._fieldMaterial._fieldShininess = 300;
        varObjBg2._fieldMaterial._fieldReflective = 0.9;
        varObjBg2._fieldMaterial._fieldTransparency = 0.9;
        varObjBg2._fieldMaterial._fieldRefractiveIndex = 1.0000034;
        varEnv.SetObject(varObjBg2);

        Canvas img = varCamera.RenderCanvas(varEnv);
        img.RenderFile("RayTracerChallenge__Chapter11__SphereWithinSphere" + argSpecs);
    }
    void ExampleMirrorRoom()
    {
        Camera varCamera = new Camera(400,200,1.152);
        varCamera.SetTransform(new ViewMatrix(new Point(-2.6,1.5,-3.9), new Point(-0.6,1,-0.8), new Vector(0,1,0)));

        World varEnv = new World();

        PointSource varLight = new PointSource(new Point(-4.9,4.9,-1), new Color(1,1,1));
        varEnv.SetLight(varLight);

        Material varWallMaterial = new Material();
        varWallMaterial.SetPattern(new PatternStripe(new Color(0.45,0.45,0.45), new Color(0.55,0.55,0.55)));
        // varWallMaterial._fieldPattern.SetTransform(new ScalingMatrix(0.25,0.25,0.25).GetRotateY(1.5708));
        varWallMaterial._fieldPattern.SetTransform(new YRotationMatrix(1.5708).GetScale(0.25,0.25,0.25));
        varWallMaterial._fieldAmbient = 0;
        varWallMaterial._fieldDiffuse = 0.4;
        varWallMaterial._fieldSpecular = 0;
        varWallMaterial._fieldReflective = 0.3;

        UnitPlane varFloor = new UnitPlane();
        varFloor.SetTransform(new YRotationMatrix(0.31415));
        varFloor.SetMaterial(new Material());
        varFloor._fieldMaterial.SetPattern(new PatternChecker(new Color(0.35,0.35,0.35), new Color(0.65,0.65,0.65)));
        varFloor._fieldMaterial._fieldSpecular = 0;
        varFloor._fieldMaterial._fieldReflective = 0.4;
        varEnv.SetObject(varFloor);

        UnitPlane varCeiling = new UnitPlane();
        varCeiling.SetTransform(new TranslationMatrix(0,5,0));
        varCeiling.SetMaterial(new Material());
        varCeiling._fieldMaterial._fieldColor = new Color(0.8,0.8,0.8);
        varCeiling._fieldMaterial._fieldAmbient = 0.3;
        varCeiling._fieldMaterial._fieldSpecular = 0;
        varEnv.SetObject(varCeiling);

        UnitPlane varWallWest = new UnitPlane();
        // varWallWest.SetTransform(new YRotationMatrix(1.5708).GetRotateZ(1.5708).GetTranslate(-5,0,0));
        varWallWest.SetTransform(new TranslationMatrix(-5,0,0).GetRotateZ(1.5708).GetRotateY(1.5708));
        varWallWest.SetMaterial(varWallMaterial);
        varEnv.SetObject(varWallWest);

        UnitPlane varWallEast = new UnitPlane();
        // varWallEast.SetTransform(new YRotationMatrix(1.5708).GetRotateZ(1.5708).GetTranslate(5,0,0));
        varWallEast.SetTransform(new TranslationMatrix(5,0,0).GetRotateZ(1.5708).GetRotateY(1.5708));
        varWallEast.SetMaterial(varWallMaterial);
        varEnv.SetObject(varWallEast);

        UnitPlane varWallNorth = new UnitPlane();
        // varWallNorth.SetTransform(new XRotationMatrix(1.5708).GetTranslate(0,0,5));
        varWallNorth.SetTransform(new TranslationMatrix(0,0,5).GetRotateX(1.5708));
        varWallNorth.SetMaterial(varWallMaterial);
        varEnv.SetObject(varWallNorth);

        UnitPlane varWallSouth = new UnitPlane();
        // varWallSouth.SetTransform(new XRotationMatrix(1.5708).GetTranslate(0,0,-5));
        varWallSouth.SetTransform(new TranslationMatrix(0,0,-5).GetRotateX(1.5708));
        varWallSouth.SetMaterial(varWallMaterial);
        varEnv.SetObject(varWallSouth);

        UnitSphere varObjBg1 = new UnitSphere();
        // varObjBg1.SetTransform(new ScalingMatrix(.4,.4,.4).GetTranslate(4.6,0.4,1));
        varObjBg1.SetTransform(new TranslationMatrix(4.6,0.4,1).GetScale(.4,.4,.4));
        varObjBg1.SetMaterial(new Material());
        varObjBg1._fieldMaterial._fieldColor = new Color(0.8,0.5,0.3);
        varObjBg1._fieldMaterial._fieldShininess = 50;
        varEnv.SetObject(varObjBg1);

        UnitSphere varObjBg2 = new UnitSphere();
        // varObjBg2.SetTransform(new ScalingMatrix(.3,.3,.3).GetTranslate((4.7,0.3,.4));
        varObjBg2.SetTransform(new TranslationMatrix(4.7,0.3,.4).GetScale(.3,.3,.3));
        varObjBg2.SetMaterial(new Material());
        varObjBg2._fieldMaterial._fieldColor = new Color(0.9,0.4,0.5);
        varObjBg2._fieldMaterial._fieldShininess = 50;
        varEnv.SetObject(varObjBg2);

        UnitSphere varObjBg3 = new UnitSphere();
        // varObjBg3.SetTransform(new ScalingMatrix(.5,.5,.5).GetTranslate(-1,0.5,4.5));
        varObjBg3.SetTransform(new TranslationMatrix(-1,0.5,4.5).GetScale(.5,.5,.5));
        varObjBg3.SetMaterial(new Material());
        varObjBg3._fieldMaterial._fieldColor = new Color(0.4,0.9,0.6);
        varObjBg3._fieldMaterial._fieldShininess = 50;
        varEnv.SetObject(varObjBg3);

        UnitSphere varObjBg4 = new UnitSphere();
        // varObjBg4.SetTransform(new ScalingMatrix(.3,.3,.3).GetTranslate(-1.7,0.3,4.7));
        varObjBg4.SetTransform(new TranslationMatrix(-1.7,0.3,4.7).GetScale(.3,.3,.3));
        varObjBg4.SetMaterial(new Material());
        varObjBg4._fieldMaterial._fieldColor = new Color(0.4,0.6,0.9);
        varObjBg4._fieldMaterial._fieldShininess = 50;
        varEnv.SetObject(varObjBg4);

        UnitSphere varObjFg1 = new UnitSphere();
        varObjFg1.SetTransform(new TranslationMatrix(-0.6,1,0.6));
        varObjFg1.SetMaterial(new Material());
        varObjFg1._fieldMaterial._fieldColor = new Color(1,0.3,0.2);
        varObjFg1._fieldMaterial._fieldSpecular = 0.4;
        varObjFg1._fieldMaterial._fieldShininess = 5;
        varEnv.SetObject(varObjFg1);

        UnitSphere varObjFg2 = new UnitSphere();
        // varObjFg2.SetTransform(new ScalingMatrix(0.7,0.7,0.7).GetTranslate(0.6,0.7,-0.6));
        varObjFg2.SetTransform(new TranslationMatrix(0.6,0.7,-0.6).GetScale(0.7,0.7,0.7));
        varObjFg2.SetMaterial(new Material());
        varObjFg2._fieldMaterial._fieldColor = new Color(0,0,0.2);
        varObjFg2._fieldMaterial._fieldAmbient = 0;
        varObjFg2._fieldMaterial._fieldDiffuse = 0.4;
        varObjFg2._fieldMaterial._fieldSpecular = 0.9;
        varObjFg2._fieldMaterial._fieldShininess = 300;
        varObjFg2._fieldMaterial._fieldReflective = 0.9;
        varObjFg2._fieldMaterial._fieldTransparency = 0.9;
        varObjFg2._fieldMaterial._fieldRefractiveIndex = 1.5;
        varEnv.SetObject(varObjFg2);

        UnitSphere varObjFg3 = new UnitSphere();
        // varObjFg3.SetTransform(new ScalingMatrix(0.5,0.5,0.5).GetTranslate(-0.7,0.5,-0.8));
        varObjFg3.SetTransform(new TranslationMatrix(-0.7,0.5,-0.8).GetScale(0.5,0.5,0.5));
        varObjFg3.SetMaterial(new Material());
        varObjFg3._fieldMaterial._fieldColor = new Color(0,0.2,0);
        varObjFg3._fieldMaterial._fieldAmbient = 0;
        varObjFg3._fieldMaterial._fieldDiffuse = 0.4;
        varObjFg3._fieldMaterial._fieldSpecular = 0.9;
        varObjFg3._fieldMaterial._fieldShininess = 300;
        varObjFg3._fieldMaterial._fieldReflective = 0.9;
        varObjFg3._fieldMaterial._fieldTransparency = 0.9;
        varObjFg3._fieldMaterial._fieldRefractiveIndex = 1.5;
        varEnv.SetObject(varObjFg3);

        Canvas img = varCamera.RenderCanvas(varEnv);
        img.RenderFile("RayTracerChallenge__Chapter11__ReflectionAndRefraction");
    }
    void ExampleCubesRoom()
    {
        Camera varCamera = new Camera(400,200,0.785);
        varCamera.SetTransform(new ViewMatrix(new Point(8,6,-8), new Point(0,3,0), new Vector(0,1,0)));

        World varEnv = new World();

        PointSource varLight = new PointSource(new Point(0,6.9,-5), new Color(1,1,0.9));
        varEnv.SetLight(varLight);

        UnitCube varFloorCeiling = new UnitCube();
        varFloorCeiling.SetTransform(new ScalingMatrix(20,7,20).GetTranslate(0,1,0));
        varFloorCeiling.SetMaterial(new Material());
        varFloorCeiling._fieldMaterial.SetPattern(new PatternChecker(new Color(0,0,0), new Color(0.25,0.25,0.25)));
        varFloorCeiling._fieldMaterial._fieldPattern.SetTransform(new ScalingMatrix(.07,.07,.07));
        varFloorCeiling._fieldMaterial._fieldAmbient = 0.25;
        varFloorCeiling._fieldMaterial._fieldDiffuse = 0.7;
        varFloorCeiling._fieldMaterial._fieldSpecular = 0.9;
        varFloorCeiling._fieldMaterial._fieldShininess = 300;
        varFloorCeiling._fieldMaterial._fieldReflective = 0.1;
        varEnv.SetObject(varFloorCeiling);

        UnitCube varWalls = new UnitCube();
        // varWallWest.SetTransform(new YRotationMatrix(1.5708).GetRotateZ(1.5708).GetTranslate(-5,0,0));
        varWalls.SetTransform(new ScalingMatrix(10,10,10));
        varWalls.SetMaterial(new Material());
        varWalls._fieldMaterial.SetPattern(new PatternChecker(new Color(0.4863, 0.3765, 0.2941), new Color(0.3725, 0.2902, 0.2275)));
        varWalls._fieldMaterial._fieldPattern.SetTransform(new ScalingMatrix(0.05,20,0.05));
        varWalls._fieldMaterial._fieldAmbient = .1;
        varWalls._fieldMaterial._fieldDiffuse = 0.7;
        varWalls._fieldMaterial._fieldSpecular = 0.9;
        varWalls._fieldMaterial._fieldShininess = 300;
        varWalls._fieldMaterial._fieldReflective = 0.1;
        varEnv.SetObject(varWalls);

        UnitCube varTableTop = new UnitCube();
        varTableTop.SetTransform(new TranslationMatrix(0,3.1,0).GetScale(3,.1,2));
        varTableTop.SetMaterial(new Material());
        varTableTop._fieldMaterial.SetPattern(new PatternStripe(new Color(0.5529,0.4235,0.3255), new Color(0.6588, 0.5098, 0.4)));
        varTableTop._fieldMaterial._fieldPattern.SetTransform(new ScalingMatrix(0.05,0.05,0.05).GetRotateY(0.1));
        varTableTop._fieldMaterial._fieldAmbient = 0.1;
        varTableTop._fieldMaterial._fieldDiffuse = 0.7;
        varTableTop._fieldMaterial._fieldSpecular = 0.9;
        varTableTop._fieldMaterial._fieldShininess = 300;
        varTableTop._fieldMaterial._fieldReflective = 0.2;
        varEnv.SetObject(varTableTop);

        UnitCube varTableLeg1 = new UnitCube();
        varTableLeg1.SetTransform(new TranslationMatrix(2.7,1.5,-1.7).GetScale(.1,1.5,.1));
        varTableLeg1.SetMaterial(new Material());
        varTableLeg1._fieldMaterial._fieldColor = new Color(0.5529,0.4235,0.3255);
        varTableLeg1._fieldMaterial._fieldAmbient = 0.2;
        varTableLeg1._fieldMaterial._fieldDiffuse = 0.7;
        varEnv.SetObject(varTableLeg1);

        UnitCube varTableLeg2 = new UnitCube();
        varTableLeg2.SetTransform(new TranslationMatrix(2.7,1.5,1.7).GetScale(.1,1.5,.1));
        varTableLeg2.SetMaterial(new Material());
        varTableLeg2._fieldMaterial._fieldColor = new Color(0.5529,0.4235,0.3255);
        varTableLeg2._fieldMaterial._fieldAmbient = 0.2;
        varTableLeg2._fieldMaterial._fieldDiffuse = 0.7;
        varEnv.SetObject(varTableLeg2);

        UnitCube varTableLeg3 = new UnitCube();
        varTableLeg3.SetTransform(new TranslationMatrix(-2.7,1.5,-1.7).GetScale(.1,1.5,.1));
        varTableLeg3.SetMaterial(new Material());
        varTableLeg3._fieldMaterial._fieldColor = new Color(0.5529,0.4235,0.3255);
        varTableLeg3._fieldMaterial._fieldAmbient = 0.2;
        varTableLeg3._fieldMaterial._fieldDiffuse = 0.7;
        varEnv.SetObject(varTableLeg3);

        UnitCube varTableLeg4 = new UnitCube();
        varTableLeg4.SetTransform(new TranslationMatrix(-2.7,1.5,1.7).GetScale(.1,1.5,.1));
        varTableLeg4.SetMaterial(new Material());
        varTableLeg4._fieldMaterial._fieldColor = new Color(0.5529,0.4235,0.3255);
        varTableLeg4._fieldMaterial._fieldAmbient = 0.2;
        varTableLeg4._fieldMaterial._fieldDiffuse = 0.7;
        varEnv.SetObject(varTableLeg4);

        UnitCube varCubeGlass = new UnitCube();
        varCubeGlass.SetTransform(new TranslationMatrix(0,3.45001,0).GetRotateY(0.2).GetScale(.25,.25,.25));
        varCubeGlass._fieldCastsShadow = false;
        varCubeGlass.SetMaterial(new Material());
        varCubeGlass._fieldMaterial._fieldColor = new Color(1,1,0.8);
        varCubeGlass._fieldMaterial._fieldAmbient = 0;
        varCubeGlass._fieldMaterial._fieldDiffuse = 0.3;
        varCubeGlass._fieldMaterial._fieldSpecular = 0.9;
        varCubeGlass._fieldMaterial._fieldShininess = 300;
        varCubeGlass._fieldMaterial._fieldReflective = 0.7;
        varCubeGlass._fieldMaterial._fieldTransparency = 0.7;
        varCubeGlass._fieldMaterial._fieldRefractiveIndex = 1.5;
        varEnv.SetObject(varCubeGlass);

        UnitCube varCubeMini1 = new UnitCube();
        varCubeMini1.SetTransform(new TranslationMatrix(1,3.35,-0.9).GetRotateY(-0.4).GetScale(.15,.15,.15));
        varCubeMini1.SetMaterial(new Material());
        varCubeMini1._fieldMaterial._fieldColor = new Color(1,.5,.5);
        varCubeMini1._fieldMaterial._fieldDiffuse = 0.4;
        varCubeMini1._fieldMaterial._fieldReflective = 0.6;
        varEnv.SetObject(varCubeMini1);

        UnitCube varCubeMini2 = new UnitCube();
        varCubeMini2.SetTransform(new TranslationMatrix(-1.5,3.27,.3).GetRotateY(0.4).GetScale(.15,.07,.15));
        varCubeMini2.SetMaterial(new Material());
        varCubeMini2._fieldMaterial._fieldColor = new Color(1,1,.5);
        varEnv.SetObject(varCubeMini2);

        UnitCube varCubeMini3 = new UnitCube();
        varCubeMini3.SetTransform(new TranslationMatrix(0,3.25,1).GetRotateY(0.4).GetScale(.2,0.05,0.05));
        varCubeMini3.SetMaterial(new Material());
        varCubeMini3._fieldMaterial._fieldColor = new Color(.5,1,.5);
        varEnv.SetObject(varCubeMini3);

        UnitCube varCubeMini4 = new UnitCube();
        varCubeMini4.SetTransform(new TranslationMatrix(-0.6,3.4,-1).GetRotateY(0.8).GetScale(.05,.2,.05));
        varCubeMini4.SetMaterial(new Material());
        varCubeMini4._fieldMaterial._fieldColor = new Color(.5,.5,1);
        varEnv.SetObject(varCubeMini4);

        UnitCube varCubeMini5 = new UnitCube();
        varCubeMini5.SetTransform(new TranslationMatrix(2,3.4,1).GetRotateY(0.8).GetScale(.05,.2,.05));
        varCubeMini5.SetMaterial(new Material());
        varCubeMini5._fieldMaterial._fieldColor = new Color(.5,1,1);
        varEnv.SetObject(varCubeMini5);

        UnitCube varFrame1 = new UnitCube();
        varFrame1.SetTransform(new TranslationMatrix(-10,4,1).GetScale(.05,1,1));
        varFrame1.SetMaterial(new Material());
        varFrame1._fieldMaterial._fieldColor = new Color(.7098,.2471,.2196);
        varFrame1._fieldMaterial._fieldDiffuse = 0.6;
        varEnv.SetObject(varFrame1);

        UnitCube varFrame2 = new UnitCube();
        varFrame2.SetTransform(new TranslationMatrix(-10,3.4,2.7).GetScale(.05,.4,.4));
        varFrame2.SetMaterial(new Material());
        varFrame2._fieldMaterial._fieldColor = new Color(.2667,.2706,.6902);
        varFrame2._fieldMaterial._fieldDiffuse = 0.6;
        varEnv.SetObject(varFrame2);

        UnitCube varFrame3 = new UnitCube();
        varFrame3.SetTransform(new TranslationMatrix(-10,4.6,2.7).GetScale(.05,.4,.4));
        varFrame3.SetMaterial(new Material());
        varFrame3._fieldMaterial._fieldColor = new Color(.3098,.5961,.3098);
        varFrame3._fieldMaterial._fieldDiffuse = 0.6;
        varEnv.SetObject(varFrame3);

        UnitCube varMirror = new UnitCube();
        varMirror.SetTransform(new TranslationMatrix(-2,3.5,9.95).GetScale(4.8,1.4,.06));
        varMirror.SetMaterial(new Material());
        varMirror._fieldMaterial._fieldColor = new Color(0,0,0);
        varMirror._fieldMaterial._fieldDiffuse = 0;
        varMirror._fieldMaterial._fieldAmbient = 0;
        varMirror._fieldMaterial._fieldSpecular = 1;
        varMirror._fieldMaterial._fieldShininess = 300;
        varMirror._fieldMaterial._fieldReflective = 1;
        varEnv.SetObject(varMirror);

        UnitCube varMirrorFrame = new UnitCube();
        varMirrorFrame.SetTransform(new TranslationMatrix(-2,3.5,9.95).GetScale(5,1.5,.05));
        varMirrorFrame.SetMaterial(new Material());
        varMirrorFrame._fieldMaterial._fieldColor = new Color(.3882,.2627,.1882);
        varMirrorFrame._fieldMaterial._fieldDiffuse = 0.7;
        varEnv.SetObject(varMirrorFrame);

        Canvas img = varCamera.RenderCanvas(varEnv);
        img.RenderFile("RayTracerChallenge__Chapter12__CubesRoom");
    }
    void ExampleCylinders() {
        Camera varCamera = new Camera(400,200,0.314);
        varCamera.SetTransform(new ViewMatrix(new Point(8,3.5,-9), new Point(0,.3,0), new Vector(0,1,0)));

        World varEnv = new World();

        PointSource varLight = new PointSource(new Point(1,6.9,-4.9), new Color(1,1,1));
        varEnv.SetLight(varLight);

        UnitPlane varFloor = new UnitPlane();
        varFloor.SetMaterial(new Material());
        varFloor._fieldMaterial.SetPattern(new PatternChecker(new Color(0.5,0.5,0.5), new Color(0.75,0.75,0.75)));
        varFloor._fieldMaterial._fieldPattern.SetTransform(new YRotationMatrix(0.3).GetScale(.25,.25,.25));
        varFloor._fieldMaterial._fieldAmbient = 0.2;
        varFloor._fieldMaterial._fieldDiffuse = 0.9;
        varFloor._fieldMaterial._fieldSpecular = 0;
        varEnv.SetObject(varFloor);

        UnitCylinder varCylinder = new UnitCylinder();
        varCylinder._fieldHeightMin = 0;
        varCylinder._fieldHeightMax = 0.75;
        varCylinder._fieldClosed = true;
        varCylinder.SetTransform(new TranslationMatrix(-1,0,1).GetScale(.5,1,.5));
        varCylinder.SetMaterial(new Material());
        varCylinder._fieldMaterial._fieldColor = new Color(0,0,0.6);
        varCylinder._fieldMaterial._fieldDiffuse = 0.1;
        varCylinder._fieldMaterial._fieldSpecular = 0.9;
        varCylinder._fieldMaterial._fieldShininess = 300;
        varCylinder._fieldMaterial._fieldReflective = 0.9;
        varEnv.SetObject(varCylinder);

        UnitCylinder varCylinderConcentric1 = new UnitCylinder();
        varCylinderConcentric1._fieldHeightMin = 0;
        varCylinderConcentric1._fieldHeightMax = 0.2;
        varCylinderConcentric1._fieldClosed = false;
        varCylinderConcentric1.SetTransform(new TranslationMatrix(1,0,0).GetScale(.8,1,.8));
        varCylinderConcentric1.SetMaterial(new Material());
        varCylinderConcentric1._fieldMaterial._fieldColor = new Color(1,1,0.3);
        varCylinderConcentric1._fieldMaterial._fieldAmbient = 0.1;
        varCylinderConcentric1._fieldMaterial._fieldDiffuse = 0.8;
        varCylinderConcentric1._fieldMaterial._fieldSpecular = 0.9;
        varCylinderConcentric1._fieldMaterial._fieldShininess = 300;
        varEnv.SetObject(varCylinderConcentric1);

        UnitCylinder varCylinderConcentric2 = new UnitCylinder();
        varCylinderConcentric2._fieldHeightMin = 0;
        varCylinderConcentric2._fieldHeightMax = 0.3;
        varCylinderConcentric2._fieldClosed = false;
        varCylinderConcentric2.SetTransform(new TranslationMatrix(1,0,0).GetScale(.6,1,.6));
        varCylinderConcentric2.SetMaterial(new Material());
        varCylinderConcentric2._fieldMaterial._fieldColor = new Color(1,.9,.4);
        varCylinderConcentric2._fieldMaterial._fieldAmbient = 0.1;
        varCylinderConcentric2._fieldMaterial._fieldDiffuse = 0.8;
        varCylinderConcentric2._fieldMaterial._fieldSpecular = 0.9;
        varCylinderConcentric2._fieldMaterial._fieldShininess = 300;
        varEnv.SetObject(varCylinderConcentric2);

        UnitCylinder varCylinderConcentric3 = new UnitCylinder();
        varCylinderConcentric3._fieldHeightMin = 0;
        varCylinderConcentric3._fieldHeightMax = 0.4;
        varCylinderConcentric3._fieldClosed = false;
        varCylinderConcentric3.SetTransform(new TranslationMatrix(1,0,0).GetScale(.4,1,.4));
        varCylinderConcentric3.SetMaterial(new Material());
        varCylinderConcentric3._fieldMaterial._fieldColor = new Color(1,.8,.5);
        varCylinderConcentric3._fieldMaterial._fieldAmbient = 0.1;
        varCylinderConcentric3._fieldMaterial._fieldDiffuse = 0.8;
        varCylinderConcentric3._fieldMaterial._fieldSpecular = 0.9;
        varCylinderConcentric3._fieldMaterial._fieldShininess = 300;
        varEnv.SetObject(varCylinderConcentric3);

        UnitCylinder varCylinderConcentric4 = new UnitCylinder();
        varCylinderConcentric4._fieldHeightMin = 0;
        varCylinderConcentric4._fieldHeightMax = 0.5;
        varCylinderConcentric4._fieldClosed = true;
        varCylinderConcentric4.SetTransform(new TranslationMatrix(1,0,0).GetScale(.2,1,.2));
        varCylinderConcentric4.SetMaterial(new Material());
        varCylinderConcentric4._fieldMaterial._fieldColor = new Color(1,.7,.6);
        varCylinderConcentric4._fieldMaterial._fieldAmbient = 0.1;
        varCylinderConcentric4._fieldMaterial._fieldDiffuse = 0.8;
        varCylinderConcentric4._fieldMaterial._fieldSpecular = 0.9;
        varCylinderConcentric4._fieldMaterial._fieldShininess = 300;
        varEnv.SetObject(varCylinderConcentric4);

        UnitCylinder varCylinderRed = new UnitCylinder();
        varCylinderRed._fieldHeightMin = 0;
        varCylinderRed._fieldHeightMax = 0.3;
        varCylinderRed._fieldClosed = true;
        varCylinderRed.SetTransform(new TranslationMatrix(0,0,-0.75).GetScale(.05,1,.05));
        varCylinderRed.SetMaterial(new Material());
        varCylinderRed._fieldMaterial._fieldColor = new Color(1,0,0);
        varCylinderRed._fieldMaterial._fieldAmbient = 0.1;
        varCylinderRed._fieldMaterial._fieldDiffuse = 0.9;
        varCylinderRed._fieldMaterial._fieldSpecular = 0.9;
        varCylinderRed._fieldMaterial._fieldShininess = 300;
        varEnv.SetObject(varCylinderRed);

        UnitCylinder varCylinderRedGreen = new UnitCylinder();
        varCylinderRedGreen._fieldHeightMin = 0;
        varCylinderRedGreen._fieldHeightMax = 0.3;
        varCylinderRedGreen._fieldClosed = true;
        varCylinderRedGreen.SetTransform(new TranslationMatrix(0,0,-2.25).GetRotateY(-.15).GetTranslate(0,0,1.5).GetScale(.05,1,.05));
        varCylinderRedGreen.SetMaterial(new Material());
        varCylinderRedGreen._fieldMaterial._fieldColor = new Color(1,1,0);
        varCylinderRedGreen._fieldMaterial._fieldAmbient = 0.1;
        varCylinderRedGreen._fieldMaterial._fieldDiffuse = 0.9;
        varCylinderRedGreen._fieldMaterial._fieldSpecular = 0.9;
        varCylinderRedGreen._fieldMaterial._fieldShininess = 300;
        varEnv.SetObject(varCylinderRedGreen);

        UnitCylinder varCylinderGreen = new UnitCylinder();
        varCylinderGreen._fieldHeightMin = 0;
        varCylinderGreen._fieldHeightMax = 0.3;
        varCylinderGreen._fieldClosed = true;
        varCylinderGreen.SetTransform(new TranslationMatrix(0,0,-2.25).GetRotateY(-.3).GetTranslate(0,0,1.5).GetScale(.05,1,.05));
        varCylinderGreen.SetMaterial(new Material());
        varCylinderGreen._fieldMaterial._fieldColor = new Color(0,1,0);
        varCylinderGreen._fieldMaterial._fieldAmbient = 0.1;
        varCylinderGreen._fieldMaterial._fieldDiffuse = 0.9;
        varCylinderGreen._fieldMaterial._fieldSpecular = 0.9;
        varCylinderGreen._fieldMaterial._fieldShininess = 300;
        varEnv.SetObject(varCylinderGreen);

        UnitCylinder varCylinderGreenBlue = new UnitCylinder();
        varCylinderGreenBlue._fieldHeightMin = 0;
        varCylinderGreenBlue._fieldHeightMax = 0.3;
        varCylinderGreenBlue._fieldClosed = true;
        varCylinderGreenBlue.SetTransform(new TranslationMatrix(0,0,-2.25).GetRotateY(-.45).GetTranslate(0,0,1.5).GetScale(.05,1,.05));
        varCylinderGreenBlue.SetMaterial(new Material());
        varCylinderGreenBlue._fieldMaterial._fieldColor = new Color(0,1,1);
        varCylinderGreenBlue._fieldMaterial._fieldAmbient = 0.1;
        varCylinderGreenBlue._fieldMaterial._fieldDiffuse = 0.9;
        varCylinderGreenBlue._fieldMaterial._fieldSpecular = 0.9;
        varCylinderGreenBlue._fieldMaterial._fieldShininess = 300;
        varEnv.SetObject(varCylinderGreenBlue);

        UnitCylinder varCylinderGlass = new UnitCylinder();
        varCylinderGlass._fieldHeightMin = 0.0001;
        varCylinderGlass._fieldHeightMax = 0.5;
        varCylinderGlass._fieldClosed = true;
        varCylinderGlass.SetTransform(new TranslationMatrix(0,0,-1.5).GetScale(.33,1,.33));
        varCylinderGlass.SetMaterial(new Material());
        varCylinderGlass._fieldMaterial._fieldColor = new Color(.25,0,0);
        varCylinderGlass._fieldMaterial._fieldDiffuse = 0.1;
        varCylinderGlass._fieldMaterial._fieldSpecular = 0.9;
        varCylinderGlass._fieldMaterial._fieldShininess = 300;
        varCylinderGlass._fieldMaterial._fieldReflective = 0.9;
        varCylinderGlass._fieldMaterial._fieldTransparency = 0.9;
        varCylinderGlass._fieldMaterial._fieldRefractiveIndex = 1.5;
        varEnv.SetObject(varCylinderGlass);

        Canvas img = varCamera.RenderCanvas(varEnv);
        img.RenderFile("RayTracerChallenge__Chapter13__Cylinders");
    }

    void ExampleCompositeHexagon() {
        Camera varCamera = new Camera(400,200,1.152);
        varCamera.SetTransform(new ViewMatrix(new Point(0,2.5,-4.9), new Point(0,0,0), new Vector(0,1,0)));
        // World varEnv = new World();
        World varEnv = GetMirrorRoom();
        // PointSource varLight = new PointSource(new Point(0,4.9,2), new Color(1,1,1));
        // varEnv.SetLight(varLight);
        varEnv._fieldLights[0] = new PointSource(new Point(0,4.9,2), new Color(1,1,1));
        // Sphere varHexagonVertex = GetHexagonCompositeVertex();
        // varHexagonVertex._fieldTransform = new TranslationMatrix(0,2.5,0);
        // varEnv.SetObject(varHexagonVertex);
        // CameraRender("CompositeHexagonVertex", varEnv);
        // varEnv = GetMirrorRoom();
        // Cylinder varHexagonEdge = GetHexagonCompositeEdge();
        // varHexagonEdge._fieldTransform = new TranslationMatrix(0,2.5,0);
        // varEnv.SetObject(varHexagonEdge);
        // CameraRender("CompositeHexagonEdge", varEnv);
        // varEnv = GetMirrorRoom();
        // Group varHexagonSide = GetHexagonCompositeSide();
        // varHexagonSide._fieldTransform = new TranslationMatrix(0,2.5,0);
        // varEnv.SetObject(varHexagonSide);
        // CameraRender("CompositeHexagonSide", varEnv);
        // varEnv = GetMirrorRoom();
        CompositeGroup varHexagon = GetHexagonComposite();
        varEnv.SetObject(varHexagon);
        Canvas img = varCamera.RenderCanvas(varEnv);
        img.RenderFile("CompositeHexagon");
    }

    UnitSphere GetHexagonCompositeVertex() {
        UnitSphere varVertex = new UnitSphere();
        varVertex.SetTransform(new TranslationMatrix(0,0,-1).GetScale(0.25,0.25,0.25));
        return varVertex;
    }
    UnitCylinder GetHexagonCompositeEdge() {
        UnitCylinder varEdge = new UnitCylinder();
        varEdge._fieldHeightMin = 0;
        varEdge._fieldHeightMax = 1;
        varEdge.SetTransform(new TranslationMatrix(0,0,-1).GetRotateY(-_fieldPM.GetPI()/6).GetRotateZ(-_fieldPM.GetPI()/2).GetScale(0.25,1,0.25));
        return varEdge;
    }
    CompositeGroup GetHexagonCompositeSide() {
        CompositeGroup varHexagonSide = new CompositeGroup();
        varHexagonSide.SetObject(GetHexagonCompositeVertex());
        varHexagonSide.SetObject(GetHexagonCompositeEdge());
        return varHexagonSide;
    }
    CompositeGroup GetHexagonComposite() {
        CompositeGroup varHexagon = new CompositeGroup();
        for (int i=0; i < 6; i++) {
            CompositeGroup varHexagonSide = GetHexagonCompositeSide();
            varHexagonSide.SetTransform(new YRotationMatrix(i * _fieldPM.GetPI()/3));
            varHexagon.SetObject(varHexagonSide);
        }
        return varHexagon;
    }
    void CameraRenderViewAngledFromAbove(String argSpecs, World argWorld) {
        Camera varCamera = new Camera(400,200,1.152);
        varCamera.SetTransform(new ViewMatrix(new Point(0,2.5,-4.9), new Point(0,0,0), new Vector(0,1,0)));
        Canvas img = varCamera.RenderCanvas(argWorld);
        img.RenderFile(argSpecs);
    }
    World GetMirrorRoom()
    {
        World varEnv = new World();

        PointSource varLight = new PointSource(new Point(0,4.9,0), new Color(1,1,1));
        varEnv.SetLight(varLight);

        Material varWallMaterial = new Material();
        varWallMaterial.SetPattern(new PatternStripe(new Color(0.45,0.45,0.45), new Color(0.55,0.55,0.55)));
        // varWallMaterial._fieldPattern.SetTransform(new ScalingMatrix(0.25,0.25,0.25).GetRotateY(1.5708));
        varWallMaterial._fieldPattern.SetTransform(new YRotationMatrix(1.5708).GetScale(0.25,0.25,0.25));
        varWallMaterial._fieldAmbient = 0;
        varWallMaterial._fieldDiffuse = 0.4;
        varWallMaterial._fieldSpecular = 0;
        varWallMaterial._fieldReflective = 0.3;

        UnitPlane varFloor = new UnitPlane();
        varFloor.SetMaterial(new Material());
        varFloor._fieldMaterial.SetPattern(new PatternChecker(new Color(0.35,0.35,0.35), new Color(0.65,0.65,0.65)));
        varFloor._fieldMaterial._fieldSpecular = 0;
        varFloor._fieldMaterial._fieldReflective = 0.4;
        varEnv.SetObject(varFloor);

        UnitPlane varCeiling = new UnitPlane();
        varCeiling.SetTransform(new TranslationMatrix(0,5,0));
        varCeiling.SetMaterial(new Material());
        varCeiling._fieldMaterial._fieldColor = new Color(0.8,0.8,0.8);
        varCeiling._fieldMaterial._fieldAmbient = 0.3;
        varCeiling._fieldMaterial._fieldSpecular = 0;
        varEnv.SetObject(varCeiling);

        UnitPlane varWallWest = new UnitPlane();
        varWallWest.SetTransform(new TranslationMatrix(-5,0,0).GetRotateZ(1.5708));
        varWallWest.SetMaterial(varWallMaterial);
        varEnv.SetObject(varWallWest);

        UnitPlane varWallEast = new UnitPlane();
        varWallEast.SetTransform(new TranslationMatrix(5,0,0).GetRotateZ(1.5708));
        varWallEast.SetMaterial(varWallMaterial);
        varEnv.SetObject(varWallEast);

        UnitPlane varWallNorth = new UnitPlane();
        // varWallNorth.SetTransform(new XRotationMatrix(1.5708).GetTranslate(0,0,5));
        varWallNorth.SetTransform(new TranslationMatrix(0,0,5).GetRotateX(1.5708));
        varWallNorth.SetMaterial(varWallMaterial);
        varEnv.SetObject(varWallNorth);

        UnitPlane varWallSouth = new UnitPlane();
        // varWallSouth.SetTransform(new XRotationMatrix(1.5708).GetTranslate(0,0,-5));
        varWallSouth.SetTransform(new TranslationMatrix(0,0,-5).GetRotateX(1.5708));
        varWallSouth.SetMaterial(varWallMaterial);
        varEnv.SetObject(varWallSouth);

        return varEnv;
    }





//     // void cameraRenderSpheres()
//     // {
//     //     PointSource varLight = PointSource(Point(-10,10,-10), Color(1,1,1));

//     //     Sphere varFloor = Sphere();
//     //     varFloor.setTransform(ScalingMatrix(10,0.01,10));
//     //     varFloor.setMaterial(Material());
//     //     varFloor._fieldMaterial._fieldColor = Color(1,0.9,0.9);
//     //     varFloor._fieldMaterial._fieldSpecular = 0;

//     //     Sphere varLeftWall = Sphere();
//     //     varLeftWall.setTransform(*(*(*(TranslationMatrix(0,0,5) * YRotationMatrix(-getPI()/4)) * XRotationMatrix(getPI()/2)) * ScalingMatrix(10,0.01,10)));
//     //     varLeftWall.setMaterial(*varFloor._fieldMaterial);

//     //     Sphere varRightWall = Sphere();
//     //     varRightWall.setTransform(*(*(*(TranslationMatrix(0,0,5) * YRotationMatrix(getPI()/4)) * XRotationMatrix(getPI()/2)) * ScalingMatrix(10,0.01,10)));
//     //     varRightWall.setMaterial(*varFloor._fieldMaterial);

//     //     Sphere varObjMid = Sphere();
//     //     varObjMid.setTransform(TranslationMatrix(-0.5,1,0.5));
//     //     varObjMid.setMaterial(Material());
//     //     varObjMid._fieldMaterial._fieldColor = Color(0.1,1,0.5);
//     //     varObjMid._fieldMaterial._fieldDiffuse = 0.7;
//     //     varObjMid._fieldMaterial._fieldSpecular = 0.3;

//     //     Sphere varObjRight = Sphere();
//     //     varObjRight.setTransform(*(TranslationMatrix(1.5,0.5,-0.5) * ScalingMatrix(0.5,0.5,0.5))); 
//     //     varObjRight.setMaterial(Material());
//     //     varObjRight._fieldMaterial._fieldColor = Color(0.5,1,0.1);
//     //     varObjRight._fieldMaterial._fieldDiffuse = 0.7;
//     //     varObjRight._fieldMaterial._fieldSpecular = 0.3;

//     //     Sphere varObjLeft = Sphere();
//     //     varObjLeft.setTransform(*(TranslationMatrix(-1.5,0.33,-0.75) * ScalingMatrix(0.33,0.33,0.33))); 
//     //     varObjLeft.setMaterial(Material());
//     //     varObjLeft._fieldMaterial._fieldColor = Color(1,0.8,0.1);
//     //     varObjLeft._fieldMaterial._fieldDiffuse = 0.7;
//     //     varObjLeft._fieldMaterial._fieldSpecular = 0.3;

//     //     World varEnv = World();
//     //     varEnv.setObject(new Sphere(varFloor));
//     //     varEnv.setObject(new Sphere(varLeftWall));
//     //     varEnv.setObject(new Sphere(varRightWall));
//     //     varEnv.setObject(new Sphere(varObjMid));
//     //     varEnv.setObject(new Sphere(varObjRight));
//     //     varEnv.setObject(new Sphere(varObjLeft));
//     //     // varEnv.setObject(std::make_unique<Sphere>(varFloor));
//     //     // varEnv.setObject(std::make_unique<Sphere>(varLeftWall));
//     //     // varEnv.setObject(std::make_unique<Sphere>(varRightWall));
//     //     // varEnv.setObject(std::make_unique<Sphere>(varObjMid));
//     //     // varEnv.setObject(std::make_unique<Sphere>(varObjRight));
//     //     // varEnv.setObject(std::make_unique<Sphere>(varObjLeft));
//     //     varEnv._fieldLights.push_back(varLight);

//     //     Camera varCamera = Camera(100,50,getPI()/3);
//     //     varCamera.setTransform(new ViewMatrix(Point(0,1.5,-5), Point(0,1,0), Vector(0,1,0)));

//     //     Canvas img = varCamera.render(varEnv);
//     //     img.save();
//     // }


//     // void cameraRenderSpherePlane()
//     // {
//     //     PointSource varLight = PointSource(Point(-10,10,-10), Color(1,1,1));

//     //     Plane varFloor = Plane();
//     //     varFloor.setTransform(*(TranslationMatrix(0,-1,0) * ScalingMatrix(10,1,10)));
//     //     varFloor.setMaterial(Material());
//     //     varFloor._fieldMaterial._fieldSpecular = 1;
//     //     varFloor._fieldMaterial.setPattern(new PatternChecker(Color(0,0,0), Color(1,1,1)));
//     //     varFloor._fieldMaterial._fieldPattern.setTransform(ScalingMatrix(1,0.1,1));

//     //     Plane varLeftWall = Plane();
//     //     varLeftWall.setTransform(*(*(*(TranslationMatrix(0,0,5) * YRotationMatrix(-getPI()/4)) * XRotationMatrix(getPI()/2)) * ScalingMatrix(10,1,10)));
//     //     varLeftWall.setMaterial(Material());
//     //     varLeftWall._fieldMaterial.setPattern(new PatternGradient(Color(1,0,0), Color(0,0,1)));
//     //     varLeftWall._fieldMaterial._fieldPattern.setTransform(ScalingMatrix(1,1,2));

//     //     Plane varRightWall = Plane();
//     //     varRightWall.setTransform(*(*(*(TranslationMatrix(0,0,5) * YRotationMatrix(getPI()/4)) * XRotationMatrix(getPI()/2)) * ScalingMatrix(10,1,10)));
//     //     varRightWall.setMaterial(Material());
//     //     varRightWall._fieldMaterial.setPattern(new PatternRing(Color(1,0,0), Color(1,1,1)));
//     //     varRightWall._fieldMaterial._fieldPattern.setTransform(ScalingMatrix(0.05,0.05,0.05));

//     //     Sphere varObjMid = Sphere();
//     //     varObjMid.setTransform(TranslationMatrix(-0.5,1,0.5));
//     //     varObjMid.setMaterial(Material());
//     //     varObjMid._fieldMaterial._fieldColor = Color(0.1,1,0.5);
//     //     varObjMid._fieldMaterial._fieldDiffuse = 0.7;
//     //     varObjMid._fieldMaterial._fieldSpecular = 0.3;

//     //     Sphere varObjRight = Sphere();
//     //     varObjRight.setTransform(*(TranslationMatrix(1.5,0.5,-0.5) * ScalingMatrix(2,0.5,2))); 
//     //     varObjRight.setMaterial(Material());
//     //     varObjRight._fieldMaterial._fieldDiffuse = 0.7;
//     //     varObjRight._fieldMaterial._fieldSpecular = 0.3;
//     //     varObjRight._fieldMaterial.setPattern(new PatternStripe(Color(1,0,0), Color(0,0,1)));
//     //     varObjRight._fieldMaterial._fieldPattern.setTransform(ScalingMatrix(0.25,0.25,0.25));

//     //     Sphere varObjLeft = Sphere();
//     //     varObjLeft.setTransform(*(TranslationMatrix(-1.5,0.33,-0.75) * ScalingMatrix(0.33,0.33,0.33))); 
//     //     varObjLeft.setMaterial(Material());
//     //     varObjLeft._fieldMaterial._fieldColor = Color(1,0.8,0.1);
//     //     varObjLeft._fieldMaterial._fieldDiffuse = 0.7;
//     //     varObjLeft._fieldMaterial._fieldSpecular = 0.3;

//     //     World varEnv = World();
//     //     varEnv.setObject(new Plane(varFloor));
//     //     varEnv.setObject(new Plane(varLeftWall));
//     //     varEnv.setObject(new Plane(varRightWall));
//     //     varEnv.setObject(new Sphere(varObjMid));
//     //     varEnv.setObject(new Sphere(varObjRight));
//     //     varEnv.setObject(new Sphere(varObjLeft));
//     //     // varEnv.setObject(std::make_unique<Sphere>(varFloor));
//     //     // varEnv.setObject(std::make_unique<Sphere>(varLeftWall));
//     //     // varEnv.setObject(std::make_unique<Sphere>(varRightWall));
//     //     // varEnv.setObject(std::make_unique<Sphere>(varObjMid));
//     //     // varEnv.setObject(std::make_unique<Sphere>(varObjRight));
//     //     // varEnv.setObject(std::make_unique<Sphere>(varObjLeft));
//     //     varEnv._fieldLights.push_back(varLight);

//     //     Camera varCamera = Camera(100,50,getPI()/2);
//     //     varCamera.setTransform(new ViewMatrix(Point(0,1.5,-5), Point(0,1,0), Vector(0,1,0)));

//     //     Canvas img = varCamera.render(varEnv);
//     //     img.save();
//     // }

//     // void cameraRenderPatterns()
//     // {
//     //     PointSource varLightL = PointSource(Point(-10,10,-10), Color(1,1,1));
//     //     PointSource varLightR = PointSource(Point(10,10,-10), Color(1,1,1));

//     //     Plane varFloor = Plane();
//     //     varFloor.setTransform(TranslationMatrix(0,-1,0));
//     //     // varFloor.setTransform(*(TranslationMatrix(0,-1,0) * ScalingMatrix(1,1,1)));
//     //     varFloor.setMaterial(Material());
//     //     varFloor._fieldMaterial.setPattern(new PatternChecker(Color(0,0,0), Color(1,1,1)));
//     //     // varFloor._fieldMaterial._fieldPattern.setTransform(ScalingMatrix(10,1,10));

//     //     Plane varLeftWall = Plane();
//     //     varLeftWall.setTransform(*IdentityMatrix(4,4).translate(0,0,5).rotateY(-getPI()/4).rotateX(getPI()/2));
//     //     // varLeftWall.setTransform(*(*(*(TranslationMatrix(0,10,5) * YRotationMatrix(-getPI()/4)) * XRotationMatrix(getPI()/2)) * ScalingMatrix(0.1,1,0.1)));
//     //     varLeftWall.setMaterial(Material());
//     //     varLeftWall._fieldMaterial.setPattern(new PatternGradient(Color(1,0,0), Color(0,0,1)));
//     //     varLeftWall._fieldMaterial._fieldPattern.setTransform(ScalingMatrix(2,2,2));

//     //     Plane varRightWall = Plane();
//     //     varRightWall.setTransform(*IdentityMatrix(4,4).translate(0,0,5).rotateY(getPI()/4).rotateX(getPI()/2));
//     //     // varRightWall.setTransform(*(*(*(TranslationMatrix(0,10,5) * YRotationMatrix(getPI()/4)) * XRotationMatrix(getPI()/2)) * ScalingMatrix(0.1,1,0.1)));
//     //     varRightWall.setMaterial(Material());
//     //     varRightWall._fieldMaterial.setPattern(new PatternRing(Color(1,0,0), Color(1,1,1)));
//     //     // varRightWall._fieldMaterial._fieldPattern.setTransform(ScalingMatrix(10,1,10));

//     //     Sphere varObjMid = Sphere();
//     //     varObjMid.setTransform(IdentityMatrix(4,4).translate(0,2,-6).scale(2,2,2));
//     //     // varObjMid.setTransform(TranslationMatrix(-0.5,0.6,0.5).scale(2,0.5,2));
//     //     varObjMid.setMaterial(Material());
//     //     varObjMid._fieldMaterial._fieldColor = Color(0.1,1,0.5);
//     //     varObjMid._fieldMaterial._fieldDiffuse = 0.7;
//     //     varObjMid._fieldMaterial._fieldSpecular = 0.3;
//     //     varObjMid._fieldMaterial.setPattern(new PatternGradient(Color(1,0,0), Color(0,0,1)));
//     //     varObjMid._fieldMaterial._fieldPattern.setTransform(ScalingMatrix(2,2,2));
//     //     varObjMid._fieldMaterial._fieldReflective = 1;

//     //     Sphere varObjRight = Sphere();
//     //     varObjRight.setTransform(IdentityMatrix(4,4).translate(6,2,-6).scale(2,2,2));
//     //     // varObjRight.setTransform(*(TranslationMatrix(1.5,0.5,-0.5) * ScalingMatrix(2,0.5,2))); 
//     //     varObjRight.setMaterial(Material());
//     //     varObjRight._fieldMaterial._fieldDiffuse = 0.7;
//     //     varObjRight._fieldMaterial._fieldSpecular = 0.3;
//     //     varObjRight._fieldMaterial.setPattern(new PatternStripe(Color(1,0,0), Color(0,0,1)));
//     //     varObjRight._fieldMaterial._fieldPattern.setTransform(ScalingMatrix(0.25,0.25,0.25));
//     //     varObjRight._fieldMaterial._fieldReflective = 1;

//     //     Sphere varObjLeft = Sphere();
//     //     varObjLeft.setTransform(IdentityMatrix(4,4).translate(-6,2,-6).scale(2,2,2));
//     //     // varObjLeft.setTransform(*(TranslationMatrix(-1.5,0.33,-2) * ScalingMatrix(0.33,0.33,0.33))); 
//     //     varObjLeft.setMaterial(Material());
//     //     varObjLeft._fieldMaterial._fieldColor = Color(1,0.8,0.1);
//     //     varObjLeft._fieldMaterial._fieldDiffuse = 0.7;
//     //     varObjLeft._fieldMaterial._fieldSpecular = 0.3;
//     //     varObjLeft._fieldMaterial._fieldReflective = 1;

//     //     World varEnv = World();
//     //     varEnv.setObject(new Plane(varFloor));
//     //     varEnv.setObject(new Plane(varLeftWall));
//     //     varEnv.setObject(new Plane(varRightWall));
//     //     varEnv.setObject(new Sphere(varObjMid));
//     //     varEnv.setObject(new Sphere(varObjRight));
//     //     varEnv.setObject(new Sphere(varObjLeft));
//     //     // varEnv.setObject(std::make_unique<Sphere>(varFloor));
//     //     // varEnv.setObject(std::make_unique<Sphere>(varLeftWall));
//     //     // varEnv.setObject(std::make_unique<Sphere>(varRightWall));
//     //     // varEnv.setObject(std::make_unique<Sphere>(varObjMid));
//     //     // varEnv.setObject(std::make_unique<Sphere>(varObjRight));
//     //     // varEnv.setObject(std::make_unique<Sphere>(varObjLeft));
//     //     varEnv._fieldLights.push_back(varLightL);
//     //     //varEnv._fieldLights.push_back(varLightR);

//     //     Camera varCamera = Camera(100,50,getPI()/2);
//     //     varCamera.setTransform(new ViewMatrix(Point(0,1.5,-20), Point(0,1,0), Vector(0,1,0)));

//     //     Canvas img = varCamera.render(varEnv);
//     //     img.save();
//     // }

//     // void cameraRenderReflections()
//     // {
//     //     PointSource varLightL = PointSource(Point(-10,10,-10), Color(1,1,1));
//     //     PointSource varLightR = PointSource(Point(10,10,-10), Color(1,1,1));

//     //     Plane varFloor = Plane();
//     //     varFloor.setTransform(TranslationMatrix(0,-1,0));
//     //     // varFloor.setTransform(*(TranslationMatrix(0,-1,0) * ScalingMatrix(1,1,1)));
//     //     varFloor.setMaterial(Material());
//     //     varFloor._fieldMaterial.setPattern(new PatternChecker(Color(0,0,0), Color(1,1,1)));
//     //     // varFloor._fieldMaterial._fieldPattern.setTransform(ScalingMatrix(10,1,10));

//     //     Plane varLeftWall = Plane();
//     //     varLeftWall.setTransform(*IdentityMatrix(4,4).translate(0,0,5).rotateY(-getPI()/4).rotateX(getPI()/2));
//     //     // varLeftWall.setTransform(*(*(*(TranslationMatrix(0,10,5) * YRotationMatrix(-getPI()/4)) * XRotationMatrix(getPI()/2)) * ScalingMatrix(0.1,1,0.1)));
//     //     varLeftWall.setMaterial(Material());
//     //     varLeftWall._fieldMaterial.setPattern(new PatternGradient(Color(1,0,0), Color(0,0,1)));
//     //     varLeftWall._fieldMaterial._fieldPattern.setTransform(ScalingMatrix(2,2,2));

//     //     Plane varRightWall = Plane();
//     //     varRightWall.setTransform(*IdentityMatrix(4,4).translate(0,0,5).rotateY(getPI()/4).rotateX(getPI()/2));
//     //     // varRightWall.setTransform(*(*(*(TranslationMatrix(0,10,5) * YRotationMatrix(getPI()/4)) * XRotationMatrix(getPI()/2)) * ScalingMatrix(0.1,1,0.1)));
//     //     varRightWall.setMaterial(Material());
//     //     varRightWall._fieldMaterial.setPattern(new PatternRing(Color(1,0,0), Color(1,1,1)));
//     //     // varRightWall._fieldMaterial._fieldPattern.setTransform(ScalingMatrix(10,1,10));

//     //     Sphere varObjMid = Sphere();
//     //     varObjMid.setTransform(IdentityMatrix(4,4).translate(0,2,-6).scale(2,2,2));
//     //     // varObjMid.setTransform(TranslationMatrix(-0.5,0.6,0.5).scale(2,0.5,2));
//     //     varObjMid.setMaterial(Material());
//     //     varObjMid._fieldMaterial._fieldColor = Color(0.1,1,0.5);
//     //     varObjMid._fieldMaterial._fieldDiffuse = 0.7;
//     //     varObjMid._fieldMaterial._fieldSpecular = 0.3;
//     //     varObjMid._fieldMaterial.setPattern(new PatternGradient(Color(1,0,0), Color(0,0,1)));
//     //     varObjMid._fieldMaterial._fieldPattern.setTransform(ScalingMatrix(2,2,2));
//     //     varObjMid._fieldMaterial._fieldReflective = 1;

//     //     Sphere varObjRight = Sphere();
//     //     varObjRight.setTransform(IdentityMatrix(4,4).translate(6,2,-6).scale(2,2,2));
//     //     // varObjRight.setTransform(*(TranslationMatrix(1.5,0.5,-0.5) * ScalingMatrix(2,0.5,2))); 
//     //     varObjRight.setMaterial(Material());
//     //     varObjRight._fieldMaterial._fieldDiffuse = 0.7;
//     //     varObjRight._fieldMaterial._fieldSpecular = 0.3;
//     //     varObjRight._fieldMaterial.setPattern(new PatternStripe(Color(1,0,0), Color(0,0,1)));
//     //     varObjRight._fieldMaterial._fieldPattern.setTransform(ScalingMatrix(0.25,0.25,0.25));
//     //     varObjRight._fieldMaterial._fieldReflective = 1;

//     //     Sphere varObjLeft = Sphere();
//     //     varObjLeft.setTransform(IdentityMatrix(4,4).translate(-6,2,-6).scale(2,2,2));
//     //     // varObjLeft.setTransform(*(TranslationMatrix(-1.5,0.33,-2) * ScalingMatrix(0.33,0.33,0.33))); 
//     //     varObjLeft.setMaterial(Material());
//     //     varObjLeft._fieldMaterial._fieldColor = Color(1,0.8,0.1);
//     //     varObjLeft._fieldMaterial._fieldDiffuse = 0.7;
//     //     varObjLeft._fieldMaterial._fieldSpecular = 0.3;
//     //     varObjLeft._fieldMaterial._fieldReflective = 1;

//     //     World varEnv = World();
//     //     varEnv.setObject(new Plane(varFloor));
//     //     varEnv.setObject(new Plane(varLeftWall));
//     //     varEnv.setObject(new Plane(varRightWall));
//     //     varEnv.setObject(new Sphere(varObjMid));
//     //     varEnv.setObject(new Sphere(varObjRight));
//     //     varEnv.setObject(new Sphere(varObjLeft));
//     //     // varEnv.setObject(std::make_unique<Sphere>(varFloor));
//     //     // varEnv.setObject(std::make_unique<Sphere>(varLeftWall));
//     //     // varEnv.setObject(std::make_unique<Sphere>(varRightWall));
//     //     // varEnv.setObject(std::make_unique<Sphere>(varObjMid));
//     //     // varEnv.setObject(std::make_unique<Sphere>(varObjRight));
//     //     // varEnv.setObject(std::make_unique<Sphere>(varObjLeft));
//     //     varEnv._fieldLights.push_back(varLightL);
//     //     //varEnv._fieldLights.push_back(varLightR);

//     //     Camera varCamera = Camera(160,90,getPI()/2);
//     //     varCamera.setTransform(new ViewMatrix(Point(0,1.5,-20), Point(0,1,0), Vector(0,1,0)));

//     //     Canvas img = varCamera.render(varEnv);
//     //     img.save();
//     // }

}