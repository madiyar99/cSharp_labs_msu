#include "pch.h"
#include "mkl.h"
#include <string.h>
#include <time.h>
#include <stdio.h>
#include <cmath>
#include <iostream>
#include <chrono>
#include <ctime> 

extern "C"  _declspec(dllexport)
void VM_log_vmdLn(int n, double* arr, double* results_mkl, int& ret, double& time_relations, char* mode_inp)
{
	try
	{
		MKL_INT64 mode1 = VML_HA;
		MKL_INT64 mode2 = VML_HA;
		if (strcmp(mode_inp, "HA") == 0) {
			mode2 = VML_HA;
		}
		else if (strcmp(mode_inp, "EP") == 0) {
			mode2 = VML_EP;
		}

		auto start1 = std::chrono::system_clock::now();
		vmdLn(n, arr, results_mkl, mode1);
		auto end1 = std::chrono::system_clock::now();
		std::chrono::duration<double> between1 = end1 - start1;   //время вычисления vmdln с точностью HA


		auto start2 = std::chrono::system_clock::now();
		vmdLn(n, arr, results_mkl, mode2);
		auto end2 = std::chrono::system_clock::now();
		std::chrono::duration<double> between2 = end2 - start2;   //время вычисления vmdln с заданной точностью
		time_relations = between2.count() / between1.count();
		ret = 0;
	}
	catch (...)
	{
		ret = -1;
	}
}

extern "C"  _declspec(dllexport)
void VM_log_vmsLn(int n, double* arr, double* results_mkl, int& ret, double& time_relations, char* mode_inp)
{
	try
	{
		MKL_INT64 mode1 = VML_HA;
		MKL_INT64 mode2 = VML_HA;
		if (strcmp(mode_inp, "HA") == 0) {
			mode2 = VML_HA;
		}
		else if (strcmp(mode_inp, "EP") == 0) {
			mode2 = VML_EP;
		}

		auto start1 = std::chrono::system_clock::now();
		vmdLn(n, arr, results_mkl, mode1);
		auto end1 = std::chrono::system_clock::now();
		std::chrono::duration<double> between1 = end1 - start1;   //время вычисления vmdln с точностью HA


		float* arr_floats = new float[n];
		float* results_mkl_floats = new float[n];
		for (int i = 0; i < n; i++) {
			arr_floats[i] = (float)(arr[i]);
		}
		auto start2 = std::chrono::system_clock::now();
		vmsLn(n, arr_floats, results_mkl_floats, mode2);
		auto end2 = std::chrono::system_clock::now();
		std::chrono::duration<double> between2 = end2 - start2;   //время вычисления vmsLn с заданной точностью
		time_relations = between2.count() / between1.count();
		delete[] arr_floats;
		delete[] results_mkl_floats;

		ret = 0;
	}
	catch (...)
	{
		ret = -1;
	}
}

