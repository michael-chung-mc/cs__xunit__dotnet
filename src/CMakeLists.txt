set (SOURCES main.cpp
canvas.h canvas.cpp canvas_test_unit.cpp
color.h color.cpp color_test_unit.cpp
comparinator.h comparinator.cpp comparinator_test_unit.cpp
intersection.h intersection.cpp intersection_test_unit.cpp
matrix.h matrix.cpp matrix_test_unit.cpp
pch.h pch.cpp
ray.h ray.cpp ray_test_unit.cpp
sphere.h sphere.cpp sphere_test_unit.cpp
tuple.h tuple.cpp tuple_test_unit.cpp
light.h light.cpp light.test.cpp
material.h material.cpp material.test.cpp
)
add_executable(main ${SOURCES})
target_link_libraries(main ${GTEST_LIBRARIES} ${GTEST_MAIN_LIBRARIES} pthread)