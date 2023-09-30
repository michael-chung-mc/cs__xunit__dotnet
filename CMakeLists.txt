cmake_minimum_required(VERSION 3.27.0)
set(CMAKE_CXX_COMPILER gcc)
set(CMAKE_CXX_COMPILER g++)
project(wip__cpp__c__gtest VERSION 0.1.0 LANGUAGES C CXX)

include(CTest)
enable_testing()
find_package(GTest REQUIRED)
include_directories(${GTEST_INCLUDE_DIRS})

set(CPACK_PROJECT_NAME ${PROJECT_NAME})
set(CPACK_PROJECT_VERSION ${PROJECT_VERSION})
include(CPack)

add_subdirectory(src)