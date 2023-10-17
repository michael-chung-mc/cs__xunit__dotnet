using Xunit;
using LibForm;
using LibTuple;
using LibRay;
using LibMaterial;
using LibMatrix;
using LibComparinator;
using LibIntersection;
using LibProjectMeta;

namespace LibForm.Test;

public class FormTest
{
	Comparinator _fieldComp = new Comparinator();
	ProjectMeta _fieldPM = new ProjectMeta();
    [Fact]
	public void CanaryTest() {
		Assert.Equal(1, 1);
		Assert.True(true);
	}
    [Fact]
	public void Default_Form__Empty_Parent() {
		Form varObj = new Form();
		Assert.Null(varObj._fieldParent);
	}
    [Fact]
	public void World_To_Object_Space_Conversion() {
		CompositeGroup varGroupInner = new CompositeGroup();
		varGroupInner.SetTransform(new ScalingMatrix(2,2,2));
		UnitSphere varSphere = new UnitSphere();
		varSphere.SetTransform(new TranslationMatrix(5,0,0));
		varGroupInner.SetObject(varSphere);
		Assert.True(varGroupInner._fieldForms[0]._fieldTransform.CheckEqual(varSphere._fieldTransform));
		CompositeGroup varGroupOuter = new CompositeGroup();
		varGroupOuter.SetTransform(new YRotationMatrix(_fieldPM.GetPI()/2));
		varGroupOuter.SetObject(varGroupInner);
		Assert.True(varGroupOuter._fieldForms[0]._fieldTransform.CheckEqual(varGroupInner._fieldTransform));
		Point varPoint = new Point(-2,0,-10);
		Point varExpectedPoint = new Point(0,0,-1);
		Assert.True(_fieldComp.CheckTuple(varExpectedPoint, varGroupOuter._fieldForms[0]._fieldForms[0]._fieldTransform.GetInverse() * varGroupOuter._fieldForms[0]._fieldTransform.GetInverse() * varGroupOuter._fieldTransform.GetInverse() * varPoint));
		Point varTransformedPoint = varGroupOuter._fieldForms[0]._fieldForms[0].GetObjectPointFromWorldSpace(varPoint);
		Assert.True(_fieldComp.CheckTuple(varTransformedPoint, varExpectedPoint));
	}
    [Fact]
	public void Object_To_World_Normal_Conversion() {
		UnitSphere varSphere = new UnitSphere();
		varSphere.SetTransform(new TranslationMatrix(5,0,0));
		CompositeGroup varGroupInner = new CompositeGroup();
		varGroupInner.SetTransform(new ScalingMatrix(1,2,3));
		varGroupInner.SetObject(varSphere);
		Assert.True(varGroupInner._fieldForms[0]._fieldTransform.CheckEqual(varSphere._fieldTransform));
		CompositeGroup varGroupOuter = new CompositeGroup();
		varGroupOuter.SetTransform(new YRotationMatrix(_fieldPM.GetPI()/2));
		varGroupOuter.SetObject(varGroupInner);
		Vector varNormal = new Vector(Math.Sqrt(3)/3, Math.Sqrt(3)/3,Math.Sqrt(3)/3);
		Vector varTransformedNormal = varGroupOuter._fieldForms[0]._fieldForms[0].GetWorldNormalFromObjectSpace(varNormal);
		Vector varExpectedNormal = new Vector(0.2857, 0.4286, -0.8571);
		Assert.True(_fieldComp.CheckTuple(varTransformedNormal, varExpectedNormal));
	}
    [Fact]
	public void Child_Normal_Within_Group() {
		UnitSphere varSphere = new UnitSphere();
		varSphere.SetTransform(new TranslationMatrix(5,0,0));
		CompositeGroup varGroupInner = new CompositeGroup();
		varGroupInner.SetTransform(new ScalingMatrix(1,2,3));
		varGroupInner.SetObject(varSphere);
		Assert.True(varGroupInner._fieldForms[0]._fieldTransform.CheckEqual(varSphere._fieldTransform));
		CompositeGroup varGroupOuter = new CompositeGroup();
		varGroupOuter.SetTransform(new YRotationMatrix(_fieldPM.GetPI()/2));
		varGroupOuter.SetObject(varGroupInner);
		Point varPoint = new Point(1.7321, 1.1547, -5.5774);
		Vector varTransformedNormal = varGroupOuter._fieldForms[0]._fieldForms[0].GetNormal(varPoint);
		Vector varExpectedNormal = new Vector(0.2857, 0.4286, -0.8571);
		Assert.True(_fieldComp.CheckTuple(varTransformedNormal, varExpectedNormal));
	}
}
public class SphereTest
{
	Comparinator _fieldComp = new Comparinator();
	ProjectMeta varPM = new ProjectMeta();
    [Fact]
	public void CanaryTest() {
		Assert.Equal(1, 1);
		Assert.True(true);
	}
    [Fact]
	public void EqualityTest() {
		UnitSphere s = new UnitSphere();
		UnitSphere t = new UnitSphere();
		Assert.True(s.CheckEqual(t));
	}
    [Fact]
	public void RayIntersectTwo() {
		UnitSphere s = new UnitSphere();
		Ray r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
		Intersections varXs = s.GetIntersections(r);
		Assert.Equal(2, varXs._fieldIntersections.Count());
		Assert.Equal(4, varXs._fieldIntersections[0]._fieldTime);
		Assert.Equal(6, varXs._fieldIntersections[1]._fieldTime);
	}
    [Fact]
	public void RayIntersectTangent() {
		UnitSphere s = new UnitSphere();
		Ray r = new Ray(new Point(0, 1, -5), new Vector(0, 0, 1));
		Intersections varXs = s.GetIntersections(r);
		Assert.Equal(2, varXs._fieldIntersections.Count());
		Assert.Equal(5 , varXs._fieldIntersections[0]._fieldTime);
		Assert.Equal(5 , varXs._fieldIntersections[1]._fieldTime);
	}
    [Fact]
	public void RayIntersectMiss() {
		UnitSphere s = new UnitSphere();
		Ray r = new Ray(new Point(0, 2, -5), new Vector(0, 0, 1));
		Intersections varXs = s.GetIntersections(r);
		Assert.Empty(varXs._fieldIntersections);
	}
    [Fact]
	public void RayIntersectWithinSphere() {
		UnitSphere s = new UnitSphere();
		Ray r = new Ray(new Point(0, 0, 0),new Vector(0, 0, 1));
		Intersections varXs = s.GetIntersections(r);
		Assert.Equal(2 , varXs._fieldIntersections.Count());
		Assert.Equal(-1 , varXs._fieldIntersections[0]._fieldTime);
		Assert.Equal(1 , varXs._fieldIntersections[1]._fieldTime);
	}
    [Fact]
	public void RayIntersectBehindSphere() {
		UnitSphere s = new UnitSphere();
		Ray r = new Ray(new Point(0, 0, 5), new Vector(0, 0, 1));
		Intersections varXs = s.GetIntersections(r);
		Assert.Equal(2 , varXs._fieldIntersections.Count());
		Assert.Equal(-6 , varXs._fieldIntersections[0]._fieldTime);
		Assert.Equal(-4 , varXs._fieldIntersections[1]._fieldTime);
	}
    [Fact]
	public void RayIntersectSetsObject() {
		UnitSphere s = new UnitSphere();
		Ray r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
		Intersections varXs = s.GetIntersections(r);
		Assert.Equal(2 , varXs._fieldIntersections.Count());
		Assert.True(s.CheckEqual((varXs._fieldIntersections[0]._fieldObject)));
		Assert.True(s.CheckEqual((varXs._fieldIntersections[1]._fieldObject)));
	}
    [Fact]
	public void SphereDefaultTransformIsIdentity() {
		UnitSphere s = new UnitSphere();
		IdentityMatrix m = new IdentityMatrix(4, 4);
		Assert.True(m.CheckEqual(s._fieldTransform));
	}
    [Fact]
	public void SphereModifyTransform() {
		UnitSphere s = new UnitSphere();
		TranslationMatrix m = new TranslationMatrix(2, 3, 4);
		s.SetTransform(m);
		Assert.True(m.CheckEqual(s._fieldTransform));
	}
    [Fact]
	public void SphereIdentityDoesNotModifyIntersections() {
		UnitSphere s = new UnitSphere();
		Ray r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
		ScalingMatrix m = new ScalingMatrix(1, 1, 1);
		s.SetTransform(m);
		Assert.True(m.CheckEqual(s._fieldTransform));
		Intersections varXs = s.GetIntersections(r);
		Assert.Equal(2, varXs._fieldIntersections.Count());
		Assert.Equal(4, varXs._fieldIntersections[0]._fieldTime);
		Assert.Equal(6, varXs._fieldIntersections[1]._fieldTime);
	}
    [Fact]
	public void SphereScaledModifiesIntersections() {
		UnitSphere s = new UnitSphere();
		Ray r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
		ScalingMatrix m = new ScalingMatrix(2, 2, 2);
		s.SetTransform(m);
		Intersections varXs = s.GetIntersections(r);
		Assert.Equal(2, varXs._fieldIntersections.Count());
		Assert.Equal(3, varXs._fieldIntersections[0]._fieldTime);
		Assert.Equal(7, varXs._fieldIntersections[1]._fieldTime);
		Assert.True(_fieldComp.CheckTuple(s._fieldObjectRay._fieldOrigin, new Point(0,0,-2.5)));
		Assert.True(_fieldComp.CheckTuple(s._fieldObjectRay._fieldDirection, new Vector(0,0,0.5)));
	}
    [Fact]
	public void SphereScaledTo5Intersections() {
		UnitSphere s = new UnitSphere();
		Ray r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
		ScalingMatrix m = new ScalingMatrix(5, 5, 5);
		s.SetTransform(m);
		Assert.True(m.CheckEqual(s._fieldTransform));
		Intersections varXs = s.GetIntersections(r);
		double z = 0;
		double y = 10;
		Assert.Equal(2, varXs._fieldIntersections.Count());
		Assert.True(_fieldComp.CheckFloat(varXs._fieldIntersections[0]._fieldTime, z));
		Assert.True(_fieldComp.CheckFloat(varXs._fieldIntersections[1]._fieldTime, y));
	}
    [Fact]
	public void SphereTranslatedToMiss() {
		UnitSphere s = new UnitSphere();
		Ray r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
		TranslationMatrix m = new TranslationMatrix(5, 0, 0);
		s.SetTransform(new Matrix(m));
		Intersections varXs = s.GetIntersections(r);
		Assert.Empty(varXs._fieldIntersections);
		Assert.True(_fieldComp.CheckTuple(s._fieldObjectRay._fieldOrigin, new Point(-5,0,-5)));
		Assert.True(_fieldComp.CheckTuple(s._fieldObjectRay._fieldDirection, new Vector(0,0,1)));
	}
    [Fact]
	public void SphereTranslatedAway() {
		UnitSphere s = new UnitSphere();
		Ray r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
		TranslationMatrix m = new TranslationMatrix(0, 0, 1);
		s.SetTransform(m);
		Intersections varXs = s.GetIntersections(r);
		Assert.Equal(2, varXs._fieldIntersections.Count());
		Assert.Equal(5, varXs._fieldIntersections[0]._fieldTime);
		Assert.Equal(7, varXs._fieldIntersections[1]._fieldTime);
	}
    [Fact]
	public void SphereNormalX() {
		UnitSphere s = new UnitSphere();
		Point p = new Point(1,0,0);
		Vector normal = s.GetNormal(p);
		Vector expectedV = new Vector(1,0,0);
		Assert.True(_fieldComp.CheckTuple(normal,expectedV));
	}
    [Fact]
	public void SphereNormalY() {
		UnitSphere s = new UnitSphere();
		Point p = new Point(0,1,0);
		Vector normal = s.GetNormal(p);
		Vector expectedV = new Vector(0,1,0);
		Assert.True(_fieldComp.CheckTuple(normal,expectedV));
	}
    [Fact]
	public void SphereNormalZ() {
		UnitSphere s = new UnitSphere();
		Point p = new Point(0,0,1);
		Vector normal = s.GetNormal(p);
		Vector expectedV = new Vector(0,0,1);
		Assert.True(_fieldComp.CheckTuple(normal,expectedV));
	}
    [Fact]
	public void SphereNormal() {
		UnitSphere s = new UnitSphere();
		Point p = new Point(Math.Sqrt(3)/3,Math.Sqrt(3)/3,Math.Sqrt(3)/3);
		Vector normal = s.GetNormal(p);
		Vector expectedV = new Vector(Math.Sqrt(3)/3,Math.Sqrt(3)/3,Math.Sqrt(3)/3);
		Assert.True(_fieldComp.CheckTuple(normal,expectedV));
	}
    [Fact]
	public void SphereNormalNormalized() {
		UnitSphere s = new UnitSphere();
		Point p = new Point(Math.Sqrt(3)/3,Math.Sqrt(3)/3,Math.Sqrt(3)/3);
		Vector normal = s.GetNormal(p);
		Vector expectedV = normal.GetNormal();
		Assert.True(_fieldComp.CheckTuple(normal,expectedV));
	}
    [Fact]
	public void SphereTranslatedNormalized() {
		UnitSphere s = new UnitSphere();
		Matrix t = new TranslationMatrix(0,1,0);
		s.SetTransform(t);
		Point p = new Point(0, 1.70711, -0.70711);
		Vector normal = s.GetNormal(p);
		Vector expectedV = new Vector(0, 0.70711, -0.70711);
		Assert.True(_fieldComp.CheckTuple(normal,expectedV));
	}
    [Fact]
	public void SphereTransformedNormalized() {
		UnitSphere s = new UnitSphere();
		// Matrix t = *(ScalingMatrix(1, 0.5, 1) * ZRotationMatrix(getPI()/5));
		Matrix t = new ScalingMatrix(1, 0.5, 1) * new ZRotationMatrix(varPM.GetPI()/5);
		s.SetTransform(t);
		Point p = new Point(0,Math.Sqrt(2)/2,-Math.Sqrt(2)/2);
		Vector normal = s.GetNormal(p);
		Vector expectedV = new Vector(0,0.97014,-0.24254);
		Assert.True(_fieldComp.CheckTuple(normal,expectedV));
	}

    [Fact]
	public void SphereMaterialCtor() {
		UnitSphere s = new UnitSphere();
		Material m = new Material();
		Assert.True(m.CheckEqual(s._fieldMaterial));
	}

    [Fact]
	public void SphereMaterialAssignment() {
		UnitSphere s = new UnitSphere();
		Material m = new Material();
		m._fieldAmbient = 1;
		s.SetMaterial(m);
		Assert.True(m.CheckEqual(s._fieldMaterial));
	}
}
public class SphereGlassTest {
	Comparinator _fieldComp = new Comparinator();
	ProjectMeta varPM = new ProjectMeta();
    [Fact]
	public void CanaryTest() {
		Assert.Equal(1, 1);
		Assert.True(true);
	}

    [Fact]
	public void GlassSphereCtor() {
		UnitSphereGlass varObj = new UnitSphereGlass();
		Assert.True(varObj._fieldTransform.CheckEqual(new IdentityMatrix(4,4)));
		Assert.Equal(1.0, varObj._fieldMaterial._fieldTransparency);
		Assert.Equal(1.5, varObj._fieldMaterial._fieldRefractiveIndex);
	}
}
public class PlaneTest {
	Comparinator _fieldComp = new Comparinator();
	ProjectMeta varPM = new ProjectMeta();
    [Fact]
	public void CanaryTest() {
		Assert.Equal(1, 1);
		Assert.True(true);
	}

    [Fact]
	public void PlaneNormalSameEverywhere() {
		UnitPlane varPlane = new UnitPlane();
		Vector varN1 = varPlane.GetNormal(new Point(0,0,0));
		Vector varN2 = varPlane.GetNormal(new Point(10,0,-10));
		Vector varN3 = varPlane.GetNormal(new Point(-5,0,150));
		Assert.True(_fieldComp.CheckTuple(varN1, new Vector(0,1,0)));
		Assert.True(_fieldComp.CheckTuple(varN2, new Vector(0,1,0)));
		Assert.True(_fieldComp.CheckTuple(varN3, new Vector(0,1,0)));

	}

    [Fact]
	public void RayParallelToPlane() {
		UnitPlane varPlane = new UnitPlane();
		Ray varRay = new Ray(new Point(0,10,0), new Vector(0,0,1));
		Intersections varIx = varPlane.GetIntersections(varRay);
		Assert.True(varIx._fieldIntersections.Count() == 0);
	}

    [Fact]
	public void CoplanarRayToPlane() {
		UnitPlane varPlane = new UnitPlane();
		Ray varRay = new Ray(new Point(0,0,0), new Vector(0,0,1));
		Intersections varIx = varPlane.GetIntersections(varRay);
		Assert.True(varIx._fieldIntersections.Count() == 0);
	}
    [Fact]
	public void RayIntersectingPlaneAbove() {
		UnitPlane varPlane = new UnitPlane();
		Ray varRay = new Ray(new Point(0,1,0), new Vector(0,-1,0));
		Intersections varIx = varPlane.GetIntersections(varRay);
		Assert.True(varIx._fieldIntersections.Count() == 1);
		Assert.Equal(1, varIx._fieldIntersections[0]._fieldTime);
		Assert.True(varIx._fieldIntersections[0]._fieldObject.CheckEqual(varPlane));
	}
    [Fact]
	public void RayIntersectingPlaneBelow() {
		UnitPlane varPlane = new UnitPlane();
		Ray varRay = new Ray(new Point(0,-1,0), new Vector(0,1,0));
		Intersections varIx = varPlane.GetIntersections(varRay);
		Assert.True(varIx._fieldIntersections.Count() == 1);
		Assert.Equal(1, varIx._fieldIntersections[0]._fieldTime);
		Assert.True(varIx._fieldIntersections[0]._fieldObject.CheckEqual(varPlane));
	}
}
public class AABBTest {
	Comparinator _fieldComp = new Comparinator();
	ProjectMeta varPM = new ProjectMeta();
    [Fact]
	public void CanaryTest() {
		Assert.Equal(1, 1);
		Assert.True(true);
	}
    [Fact]
	public void Ray_Intersect_Cube__Positive_X() {
		UnitAABBox varCube = new UnitAABBox();
		Ray varRay = new Ray(new Point(5,0.5,0), new Vector(-1,0,0));
		List<Intersection> varXs = varCube.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
		Assert.Equal(4, varXs[0]._fieldTime);
		Assert.Equal(6, varXs[1]._fieldTime);
	}
    [Fact]
	public void Ray_Intersect_Cube__Negative_X() {
		UnitAABBox varCube = new UnitAABBox();
		Ray varRay = new Ray(new Point(-5,0.5,0), new Vector(1,0,0));
		List<Intersection> varXs = varCube.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
		Assert.Equal(4, varXs[0]._fieldTime);
		Assert.Equal(6, varXs[1]._fieldTime);
	}
    [Fact]
	public void Ray_Intersect_Cube__Positive_Y() {
		UnitAABBox varCube = new UnitAABBox();
		Ray varRay = new Ray(new Point(.5,5,0), new Vector(0,-1,0));
		List<Intersection> varXs = varCube.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
		Assert.Equal(4, varXs[0]._fieldTime);
		Assert.Equal(6, varXs[1]._fieldTime);
	}
    [Fact]
	public void Ray_Intersect_Cube__Negative_Y() {
		UnitAABBox varCube = new UnitAABBox();
		Ray varRay = new Ray(new Point(.5,-5,0), new Vector(0,1,0));
		List<Intersection> varXs = varCube.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
		Assert.Equal(4, varXs[0]._fieldTime);
		Assert.Equal(6, varXs[1]._fieldTime);
	}
    [Fact]
	public void Ray_Intersect_Cube__Positive_Z() {
		UnitAABBox varCube = new UnitAABBox();
		Ray varRay = new Ray(new Point(.5,0,5), new Vector(0,0,-1));
		List<Intersection> varXs = varCube.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
		Assert.Equal(4, varXs[0]._fieldTime);
		Assert.Equal(6, varXs[1]._fieldTime);
	}
    [Fact]
	public void Ray_Intersect_Cube__Negative_Z() {
		UnitAABBox varCube = new UnitAABBox();
		Ray varRay = new Ray(new Point(.5,0,-5), new Vector(0,0,1));
		List<Intersection> varXs = varCube.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
		Assert.Equal(4, varXs[0]._fieldTime);
		Assert.Equal(6, varXs[1]._fieldTime);
	}
    [Fact]
	public void Ray_Intersect_Cube__Interior() {
		UnitAABBox varCube = new UnitAABBox();
		Ray varRay = new Ray(new Point(0,0.5,0), new Vector(0,0,1));
		List<Intersection> varXs = varCube.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
		Assert.Equal(-1, varXs[0]._fieldTime);
		Assert.Equal(1, varXs[1]._fieldTime);
	}
    [Fact]
	public void Ray_Miss_Cube__Away_X() {
		UnitAABBox varCube = new UnitAABBox();
		Ray varRay = new Ray(new Point(-2,0,0), new Vector(0.2673,0.5345,0.8018));
		List<Intersection> varXs = varCube.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
    [Fact]
	public void Ray_Miss_Cube__Away_Y() {
		UnitAABBox varCube = new UnitAABBox();
		Ray varRay = new Ray(new Point(0,-2,0), new Vector(0.8018,0.2673,0.5345));
		List<Intersection> varXs = varCube.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
    [Fact]
	public void Ray_Miss_Cube__Away_Z() {
		UnitAABBox varCube = new UnitAABBox();
		Ray varRay = new Ray(new Point(0,0,-2), new Vector(0.5345, 0.8018,0.2673));
		List<Intersection> varXs = varCube.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
    [Fact]
	public void Ray_Miss_Cube__Parallel_X_Z() {
		UnitAABBox varCube = new UnitAABBox();
		Ray varRay = new Ray(new Point(2,0,2), new Vector(0,0,-1));
		List<Intersection> varXs = varCube.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
    [Fact]
	public void Ray_Miss_Cube__Parallel_Y_Z() {
		UnitAABBox varCube = new UnitAABBox();
		Ray varRay = new Ray(new Point(0,2,2), new Vector(0,-1,0));
		List<Intersection> varXs = varCube.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
    [Fact]
	public void Ray_Miss_Cube__Parallel_X_Y() {
		UnitAABBox varCube = new UnitAABBox();
		Ray varRay = new Ray(new Point(2,2,0), new Vector(-1,0,0));
		List<Intersection> varXs = varCube.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
    [Fact]
	public void Cube_Normal__Positive_X() {
		UnitAABBox varCube = new UnitAABBox();
		Point varPoint = new Point(1,0.5,-0.8);
		Vector varNormal = varCube.GetNormalLocal(varPoint);
		Vector varExpectedNormal = new Vector(1,0,0);
		Assert.True(_fieldComp.CheckTuple(varExpectedNormal,varNormal));
	}
    [Fact]
	public void Cube_Normal__Negative_X() {
		UnitAABBox varCube = new UnitAABBox();
		Point varPoint = new Point(-1,-0.2,0.9);
		Vector varNormal = varCube.GetNormalLocal(varPoint);
		Vector varExpectedNormal = new Vector(-1,0,0);
		Assert.True(_fieldComp.CheckTuple(varExpectedNormal,varNormal));
	}
    [Fact]
	public void Cube_Normal__Positive_Y() {
		UnitAABBox varCube = new UnitAABBox();
		Point varPoint = new Point(-0.4,1,-0.1);
		Vector varNormal = varCube.GetNormalLocal(varPoint);
		Vector varExpectedNormal = new Vector(0,1,0);
		Assert.True(_fieldComp.CheckTuple(varExpectedNormal,varNormal));
	}
    [Fact]
	public void Cube_Normal__Negative_Y() {
		UnitAABBox varCube = new UnitAABBox();
		Point varPoint = new Point(0.3,-1,1);
		Vector varNormal = varCube.GetNormalLocal(varPoint);
		Vector varExpectedNormal = new Vector(0,-1,0);
		Assert.True(_fieldComp.CheckTuple(varExpectedNormal,varNormal));
	}
    [Fact]
	public void Cube_Normal__Positive_Z() {
		UnitAABBox varCube = new UnitAABBox();
		Point varPoint = new Point(-0.6,0.3,1);
		Vector varNormal = varCube.GetNormalLocal(varPoint);
		Vector varExpectedNormal = new Vector(0,0,1);
		Assert.True(_fieldComp.CheckTuple(varExpectedNormal,varNormal));
	}
    [Fact]
	public void Cube_Normal__Negative_Z() {
		UnitAABBox varCube = new UnitAABBox();
		Point varPoint = new Point(0.4,0.4,-1);
		Vector varNormal = varCube.GetNormalLocal(varPoint);
		Vector varExpectedNormal = new Vector(0,0,-1);
		Assert.True(_fieldComp.CheckTuple(varExpectedNormal,varNormal));
	}
    [Fact]
	public void Cube_Normal__Corner_X() {
		UnitAABBox varCube = new UnitAABBox();
		Point varPoint = new Point(1,1,1);
		Vector varNormal = varCube.GetNormalLocal(varPoint);
		Vector varExpectedNormal = new Vector(1,0,0);
		Assert.True(_fieldComp.CheckTuple(varExpectedNormal,varNormal));
	}
    [Fact]
	public void Cube_Normal__Corner_Negative_X() {
		UnitAABBox varCube = new UnitAABBox();
		Point varPoint = new Point(-1,-1,-1);
		Vector varNormal = varCube.GetNormalLocal(varPoint);
		Vector varExpectedNormal = new Vector(-1,0,0);
		Assert.True(_fieldComp.CheckTuple(varExpectedNormal,varNormal));
	}
}

public class CylinderTest {
	Comparinator _fieldComp = new Comparinator();
	ProjectMeta varPM = new ProjectMeta();
    [Fact]
	public void CanaryTest() {
		Assert.Equal(1, 1);
		Assert.True(true);
	}
    [Fact]
	public void Cylinder_Intersection_Miss__On_Surface() {
		UnitCylinder varObj = new UnitCylinder();
		Vector varDirection = new Vector(0,1,0).GetNormal();
		Ray varRay = new Ray(new Point(1,0,0), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
    [Fact]
	public void Cylinder_Intersection_Miss__Inside_Surface() {
		UnitCylinder varObj = new UnitCylinder();
		Vector varDirection = new Vector(0,1,0).GetNormal();
		Ray varRay = new Ray(new Point(0,0,0), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
    [Fact]
	public void Cylinder_Intersection_Miss__Away_Surface() {
		UnitCylinder varObj = new UnitCylinder();
		Vector varDirection = new Vector(1,1,1).GetNormal();
		Ray varRay = new Ray(new Point(0,0,-5), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
    [Fact]
	public void Cylinder_Intersection_Hit__Tangent() {
		UnitCylinder varObj = new UnitCylinder();
		Vector varDirection = new Vector(0,0,1).GetNormal();
		Ray varRay = new Ray(new Point(1,0,-5), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
		Assert.Equal(5, varXs[0]._fieldTime);
		Assert.Equal(5, varXs[1]._fieldTime);
	}
    [Fact]
	public void Cylinder_Intersection_Hit__Perpendicular() {
		UnitCylinder varObj = new UnitCylinder();
		Vector varDirection = new Vector(0,0,1).GetNormal();
		Ray varRay = new Ray(new Point(0,0,-5), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
		Assert.Equal(4, varXs[0]._fieldTime);
		Assert.Equal(6, varXs[1]._fieldTime);
	}
    [Fact]
	public void Cylinder_Intersection_Hit__Skewed() {
		UnitCylinder varObj = new UnitCylinder();
		Vector varDirection = new Vector(0.1,1,1).GetNormal();
		Ray varRay = new Ray(new Point(0.5,0,-5), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
		Assert.True(_fieldComp.CheckFloat(6.80798, varXs[0]._fieldTime));
		Assert.True(_fieldComp.CheckFloat(7.08872, varXs[1]._fieldTime));
	}
    [Fact]
	public void Cylinder_Normal__Positive_X() {
		UnitCylinder varObj = new UnitCylinder();
		Vector varNormal = varObj.GetNormalLocal(new Point(1,0,0));
		Assert.True(_fieldComp.CheckTuple(varNormal, new Vector(1,0,0)));
	}
    [Fact]
	public void Cylinder_Normal__Negative_Z() {
		UnitCylinder varObj = new UnitCylinder();
		Vector varNormal = varObj.GetNormalLocal(new Point(0,5,-1));
		Assert.True(_fieldComp.CheckTuple(varNormal, new Vector(0,0,-1)));
	}
    [Fact]
	public void Cylinder_Normal__Positive_Z() {
		UnitCylinder varObj = new UnitCylinder();
		Vector varNormal = varObj.GetNormalLocal(new Point(0,-2,1));
		Assert.True(_fieldComp.CheckTuple(varNormal, new Vector(0,0,1)));
	}
    [Fact]
	public void Cylinder_Normal__Negative_x() {
		UnitCylinder varObj = new UnitCylinder();
		Vector varNormal = varObj.GetNormalLocal(new Point(-1,1,0));
		Assert.True(_fieldComp.CheckTuple(varNormal, new Vector(-1,0,0)));
	}
    [Fact]
	public void Cylinder_Height__Default_Infinity() {
		UnitCylinder varObj = new UnitCylinder();
		Assert.Equal(double.MaxValue, varObj._fieldHeightMax);
		Assert.Equal(double.MinValue, varObj._fieldHeightMin);
	}
    [Fact]
	public void Cylinder_Height_Constrained_1_2__Intersection_Angled() {
		UnitCylinder varObj = new UnitCylinder();
		varObj._fieldHeightMax = 2;
		varObj._fieldHeightMin = 1;
		Vector varDirection = new Vector(0.1,1,0).GetNormal();
		Ray varRay = new Ray(new Point(0,1.5,0), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
    [Fact]
	public void Cylinder_Height_Constrained_1_2__Intersection_Above() {
		UnitCylinder varObj = new UnitCylinder();
		varObj._fieldHeightMax = 2;
		varObj._fieldHeightMin = 1;
		Vector varDirection = new Vector(0,0,1).GetNormal();
		Ray varRay = new Ray(new Point(0,3,-5), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
    [Fact]
	public void Cylinder_Height_Constrained_1_2__Intersection_Below() {
		UnitCylinder varObj = new UnitCylinder();
		varObj._fieldHeightMax = 2;
		varObj._fieldHeightMin = 1;
		Vector varDirection = new Vector(0,0,1).GetNormal();
		Ray varRay = new Ray(new Point(0,0,-5), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
    [Fact]
	public void Cylinder_Height_Constrained_1_2__Intersection_Max_Exclusive() {
		UnitCylinder varObj = new UnitCylinder();
		varObj._fieldHeightMax = 2;
		varObj._fieldHeightMin = 1;
		Vector varDirection = new Vector(0,0,1).GetNormal();
		Ray varRay = new Ray(new Point(0,2,-5), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
    [Fact]
	public void Cylinder_Height_Constrained_1_2__Intersection_Min_Exclusive() {
		UnitCylinder varObj = new UnitCylinder();
		varObj._fieldHeightMax = 2;
		varObj._fieldHeightMin = 1;
		Vector varDirection = new Vector(0,0,1).GetNormal();
		Ray varRay = new Ray(new Point(0,1,-5), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
    [Fact]
	public void Cylinder_Height_Constrained_1_2__Intersection_Hit() {
		UnitCylinder varObj = new UnitCylinder();
		varObj._fieldHeightMax = 2;
		varObj._fieldHeightMin = 1;
		Vector varDirection = new Vector(0,0,1).GetNormal();
		Ray varRay = new Ray(new Point(0,1.5,-2), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
	}
    [Fact]
	public void Closed_Cylinder_Default() {
		UnitCylinder varObj = new UnitCylinder();
		Assert.False(varObj._fieldClosed);
	}
    [Fact]
	public void Closed_Cylinder_Height_Constrained_1_2__Intersection_Above_Middle__2_Intersections() {
		UnitCylinder varObj = new UnitCylinder();
		varObj._fieldHeightMax = 2;
		varObj._fieldHeightMin = 1;
		varObj._fieldClosed = true;
		Vector varDirection = new Vector(0,-1,0).GetNormal();
		Ray varRay = new Ray(new Point(0,3,0), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
	}
    [Fact]
	public void Closed_Cylinder_Height_Constrained_1_2__Intersection_Above__2_Intersections() {
		UnitCylinder varObj = new UnitCylinder();
		varObj._fieldHeightMax = 2;
		varObj._fieldHeightMin = 1;
		varObj._fieldClosed = true;
		Vector varDirection = new Vector(0,-1,2).GetNormal();
		Ray varRay = new Ray(new Point(0,3,-2), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
	}
    [Fact]
	public void Closed_Cylinder_Height_Constrained_1_2__Intersection_Above_Corner() {
		UnitCylinder varObj = new UnitCylinder();
		varObj._fieldHeightMax = 2;
		varObj._fieldHeightMin = 1;
		varObj._fieldClosed = true;
		Vector varDirection = new Vector(0,-1,1).GetNormal();
		Ray varRay = new Ray(new Point(0,4,-2), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
	}
    [Fact]
	public void Closed_Cylinder_Height_Constrained_1_2__Intersection_Below_2_Intersections() {
		UnitCylinder varObj = new UnitCylinder();
		varObj._fieldHeightMax = 2;
		varObj._fieldHeightMin = 1;
		varObj._fieldClosed = true;
		Vector varDirection = new Vector(0,1,2).GetNormal();
		Ray varRay = new Ray(new Point(0,0,-2), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
	}
    [Fact]
	public void Closed_Cylinder_Height_Constrained_1_2__Intersection_Below_Corner() {
		UnitCylinder varObj = new UnitCylinder();
		varObj._fieldHeightMax = 2;
		varObj._fieldHeightMin = 1;
		varObj._fieldClosed = true;
		Vector varDirection = new Vector(0,1,1).GetNormal();
		Ray varRay = new Ray(new Point(0,-1,-2), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
	}
    [Fact]
	public void Closed_Cylinder_Height_Constrained_1_2__Normal_Cap_Below_Negative() {
		UnitCylinder varObj = new UnitCylinder();
		varObj._fieldHeightMax = 2;
		varObj._fieldHeightMin = 1;
		varObj._fieldClosed = true;
		Vector varDirection = varObj.GetNormalLocal(new Point(0,1,0));
		Assert.True(_fieldComp.CheckTuple(varDirection, new Vector(0,-1,0)));
	}
    [Fact]
	public void Closed_Cylinder_Height_Constrained_1_2__Normal_Cap_Right_Below_Negative() {
		UnitCylinder varObj = new UnitCylinder();
		varObj._fieldHeightMax = 2;
		varObj._fieldHeightMin = 1;
		varObj._fieldClosed = true;
		Vector varDirection = varObj.GetNormalLocal(new Point(0.5,1,0));
		Assert.True(_fieldComp.CheckTuple(varDirection, new Vector(0,-1,0)));
	}
    [Fact]
	public void Closed_Cylinder_Height_Constrained_1_2__Normal_Cap_Left_Below_Negative() {
		UnitCylinder varObj = new UnitCylinder();
		varObj._fieldHeightMax = 2;
		varObj._fieldHeightMin = 1;
		varObj._fieldClosed = true;
		Vector varDirection = varObj.GetNormalLocal(new Point(0,1,0.5));
		Assert.True(_fieldComp.CheckTuple(varDirection, new Vector(0,-1,0)));
	}
    [Fact]
	public void Closed_Cylinder_Height_Constrained_1_2__Normal_Cap_Above_Positive() {
		UnitCylinder varObj = new UnitCylinder();
		varObj._fieldHeightMax = 2;
		varObj._fieldHeightMin = 1;
		varObj._fieldClosed = true;
		Vector varDirection = varObj.GetNormalLocal(new Point(0,2,0));
		Assert.True(_fieldComp.CheckTuple(varDirection, new Vector(0,1,0)));
	}
    [Fact]
	public void Closed_Cylinder_Height_Constrained_1_2__Normal_Cap_Right_Above_Positive() {
		UnitCylinder varObj = new UnitCylinder();
		varObj._fieldHeightMax = 2;
		varObj._fieldHeightMin = 1;
		varObj._fieldClosed = true;
		Vector varDirection = varObj.GetNormalLocal(new Point(0.5,2,0));
		Assert.True(_fieldComp.CheckTuple(varDirection, new Vector(0,1,0)));
	}
    [Fact]
	public void Closed_Cylinder_Height_Constrained_1_2__Normal_Cap_Left_Above_Positive() {
		UnitCylinder varObj = new UnitCylinder();
		varObj._fieldHeightMax = 2;
		varObj._fieldHeightMin = 1;
		varObj._fieldClosed = true;
		Vector varDirection = varObj.GetNormalLocal(new Point(0,2,0.5));
		Assert.True(_fieldComp.CheckTuple(varDirection, new Vector(0,1,0)));
	}
}

public class DNConeTest {
	Comparinator _fieldComp = new Comparinator();
	ProjectMeta varPM = new ProjectMeta();
    [Fact]
	public void CanaryTest() {
		Assert.Equal(1, 1);
		Assert.True(true);
	}
    [Fact]
	public void Cone_Intersection__Below() {
		UnitDNCone varObj = new UnitDNCone();
		Vector varDirection = new Vector (0,0,1).GetNormal();
		Ray varRay = new Ray(new Point(0,0,-5), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
		Assert.True(_fieldComp.CheckFloat(5, varXs[0]._fieldTime));
		Assert.True(_fieldComp.CheckFloat(5, varXs[1]._fieldTime));
	}
    [Fact]
	public void Cone_Intersection__Below_Angled() {
		UnitDNCone varObj = new UnitDNCone();
		Vector varDirection = new Vector (1,1,1).GetNormal();
		Ray varRay = new Ray(new Point(0,0,-5), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
		Assert.True(_fieldComp.CheckFloat(8.66025, varXs[0]._fieldTime));
		Assert.True(_fieldComp.CheckFloat(8.66025, varXs[1]._fieldTime));
	}
    [Fact]
	public void Cone_Intersection__Above_Angled() {
		UnitDNCone varObj = new UnitDNCone();
		Vector varDirection = new Vector (-0.5,-1,1).GetNormal();
		Ray varRay = new Ray(new Point(1,1,-5), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
		Assert.True(_fieldComp.CheckFloat(4.55006, varXs[0]._fieldTime));
		Assert.True(_fieldComp.CheckFloat(49.44994, varXs[1]._fieldTime));
	}
    [Fact]
	public void Cone_Intersection__Only_One_Cone() {
		UnitDNCone varObj = new UnitDNCone();
		Vector varDirection = new Vector (0,1,1).GetNormal();
		Ray varRay = new Ray(new Point(0,0,-1), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Single(varXs);
		Assert.True(_fieldComp.CheckFloat(0.35355, varXs[0]._fieldTime));
	}
    [Fact]
	public void Cone_Intersection__End_Cap__Miss() {
		UnitDNCone varObj = new UnitDNCone();
		varObj._fieldHeightMin = -0.5;
		varObj._fieldHeightMax = 0.5;
		varObj._fieldClosed = true;
		Vector varDirection = new Vector (0,1,0).GetNormal();
		Ray varRay = new Ray(new Point(0,0,-5), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
    [Fact]
	public void Cone_Intersection__End_Cap__Hit_2() {
		UnitDNCone varObj = new UnitDNCone();
		varObj._fieldHeightMin = -0.5;
		varObj._fieldHeightMax = 0.5;
		varObj._fieldClosed = true;
		Vector varDirection = new Vector (0,1,1).GetNormal();
		Ray varRay = new Ray(new Point(0,0,-.25), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
	}
    [Fact]
	public void Cone_Intersection__End_Cap__Hit_4() {
		UnitDNCone varObj = new UnitDNCone();
		varObj._fieldHeightMin = -0.5;
		varObj._fieldHeightMax = 0.5;
		varObj._fieldClosed = true;
		Vector varDirection = new Vector (0,1,0).GetNormal();
		Ray varRay = new Ray(new Point(0,0,-.25), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(4, varXs.Count);
	}
    [Fact]
	public void Cone_Normal_Origin() {
		UnitDNCone varObj = new UnitDNCone();
		Vector varNormal = varObj.GetNormalLocal(new Point(0,0,0));
		Assert.True(_fieldComp.CheckTuple(varNormal, new Vector(0,0,0)));
	}
    [Fact]
	public void Cone_Normal_Above() {
		UnitDNCone varObj = new UnitDNCone();
		Vector varNormal = varObj.GetNormalLocal(new Point(1,1,1));
		Assert.True(_fieldComp.CheckTuple(varNormal, new Vector(1, -Math.Sqrt(2),1)));
	}
    [Fact]
	public void Cone_Normal_Below() {
		UnitDNCone varObj = new UnitDNCone();
		Vector varNormal = varObj.GetNormalLocal(new Point(-1,-1,0));
		Assert.True(_fieldComp.CheckTuple(varNormal, new Vector(-1, 1, 0)));
	}
}

public class GroupTest {
	Comparinator _fieldComp = new Comparinator();
	ProjectMeta _fieldPM = new ProjectMeta();
    [Fact]
	public void CanaryTest() {
		Assert.Equal(1, 1);
		Assert.True(true);
	}
    [Fact]
	public void Default_Group__Empty_List() {
		CompositeGroup varGroup = new CompositeGroup();
		Assert.True(varGroup._fieldTransform.CheckEqual(new IdentityMatrix(4,4)));
		Assert.Empty(varGroup._fieldForms);
	}
    [Fact]
	public void Add_Child_To_Group() {
		CompositeGroup varGroup = new CompositeGroup();
		Form varForm = new Form();
		varGroup.SetObject(varForm);
		Assert.Single(varGroup._fieldForms);
		Assert.Contains(varForm,varGroup._fieldForms);
		Assert.True(varGroup._fieldForms[0].CheckEqual(varGroup));
	}
    [Fact]
	public void Ray_Intersect_Empty_Group__No_Intersections() {
		CompositeGroup varGroup = new CompositeGroup();
		Ray varRay = new Ray(new Point(0,0,0), new Vector(0,0,1));
		List<Intersection> varXs = varGroup.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
    [Fact]
	public void Ray_Intersect_Group_Of_Three_Spheres__Intersects_Two() {
		CompositeGroup varGroup = new CompositeGroup();
		UnitSphere varSphereOne = new UnitSphere();
		varGroup.SetObject(varSphereOne);
		UnitSphere varSphereTwo = new UnitSphere();
		varSphereTwo.SetTransform(new TranslationMatrix(0,0,-3));
		varGroup.SetObject(varSphereTwo);
		UnitSphere varSphereThree = new UnitSphere();
		varSphereThree.SetTransform(new TranslationMatrix(5,0,0));
		varGroup.SetObject(varSphereThree);
		Ray varRay = new Ray(new Point(0,0,-5), new Vector(0,0,1));
		List<Intersection> varXs = varGroup.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(4, varXs.Count);
		Assert.True(varSphereTwo.CheckEqual(varXs[0]._fieldObject));
		Assert.True(varSphereTwo.CheckEqual(varXs[1]._fieldObject));
		Assert.True(varSphereOne.CheckEqual(varXs[2]._fieldObject));
		Assert.True(varSphereOne.CheckEqual(varXs[3]._fieldObject));
	}
    [Fact]
	public void Group_Transform_Affects_Containing_Shape() {
		CompositeGroup varGroup = new CompositeGroup();
		varGroup.SetTransform(new ScalingMatrix(2,2,2));
		UnitSphere varSphere = new UnitSphere();
		varSphere.SetTransform(new TranslationMatrix(5,0,0));
		varGroup.SetObject(varSphere);
		Ray varRay = new Ray(new Point(10,0,-10), new Vector(0,0,1));
		List<Intersection> varXs = varGroup.GetIntersections(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
	}
    // [Fact]
	// public void Group_Transform_Affects_Containing_Group() {
	// 	Group varGroupInner = new Group();
	// 	varGroupInner.SetTransform(new ScalingMatrix(2,2,2));
	// 	Sphere varSphere = new Sphere();
	// 	varSphere.SetTransform(new TranslationMatrix(5,0,0));
	// 	varGroupInner.SetObject(varSphere);
	// 	Assert.True(varGroupInner._fieldForms[0]._fieldTransform.CheckEqual(varSphere._fieldTransform));
	// 	Group varGroupOuter = new Group();
	// 	varGroupOuter.SetTransform(new YRotationMatrix(_fieldPM.GetPI()/2));
	// 	varGroupOuter.SetObject(varGroupInner);
	// 	Assert.Equal(2, varGroupOuter.GetIntersections(new Ray(new Point(10,0,-10), new Vector(0,0,1)))._fieldIntersections.Count);
	// }
}

public class TriangleTest {
	Comparinator _fieldComp = new Comparinator();
	ProjectMeta _fieldPM = new ProjectMeta();
    [Fact]
	public void CanaryTest() {
		Assert.Equal(1, 1);
		Assert.True(true);
	}
	[Fact]
	public void Default_Triangle () {
		Point varVertexOne = new Point(0,1,0);
		Point varVertexTwo = new Point(-1,0,0);
		Point varVertexThree = new Point(1,0,0);
		UnitTriangle varObj = new UnitTriangle(varVertexOne, varVertexTwo, varVertexThree);
		Assert.True(_fieldComp.CheckTuple(varObj._fieldVertexOne, varVertexOne));
		Assert.True(_fieldComp.CheckTuple(varObj._fieldVertexTwo, varVertexTwo));
		Assert.True(_fieldComp.CheckTuple(varObj._fieldVertexThree, varVertexThree));
		Assert.True(_fieldComp.CheckTuple(varObj._fieldEdgeOne, new Vector(-1,-1,0)));
		Assert.True(_fieldComp.CheckTuple(varObj._fieldEdgeTwo, new Vector(1,-1,0)));
		Assert.True(_fieldComp.CheckTuple(varObj._fieldNormal, new Vector(0,0,-1)));
	}
	[Fact]
	public void Triangle_Normal () {
		UnitTriangle varObj = new UnitTriangle(new Point(0,1,0), new Point(-1,0,0), new Point(1,0,0));
		Vector varNormalOne = varObj.GetNormalLocal(new Point(0,0.5,0));
		Vector varNormalTwo = varObj.GetNormalLocal(new Point(-0.5,0.75,0));
		Vector varNormalThree = varObj.GetNormalLocal(new Point(0.5,0.25,0));
		Assert.True(_fieldComp.CheckTuple(varObj._fieldNormal, varNormalOne));
		Assert.True(_fieldComp.CheckTuple(varObj._fieldNormal, varNormalTwo));
		Assert.True(_fieldComp.CheckTuple(varObj._fieldNormal, varNormalThree));
	}
}