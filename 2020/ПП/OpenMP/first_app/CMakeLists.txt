cmake_minimum_required(VERSION 3.17)
project(first_app)
set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -fopenmp")
set(CMAKE_CXX_STANDARD 14)

add_executable(first_app main.cpp)