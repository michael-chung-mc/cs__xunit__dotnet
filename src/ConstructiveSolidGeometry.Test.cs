using Xunit;
using LibConstructiveSolidGeometry;
using LibComparinator;
using LibProjectMeta;
using LibForm;
namespace LibConstructiveSolidGeometry.Test;

public class ConstructiveSolidGeometryTest
{
	Comparinator _fieldComp = new Comparinator();
	ProjectMeta _fieldPM = new ProjectMeta();
    [Fact]
	public void CanaryTest() {
		Assert.Equal(1, 1);
		Assert.True(true);
	}
    [Fact]
	public void CSGConstructor_WithSphereCube_ExpectSphereCube() {
		UnitSphere varSphere = new UnitSphere();
        UnitCube varCube = new UnitCube();
        ConstructiveSolidGeometry varCSG = new ConstructiveSolidGeometry();
	}
}