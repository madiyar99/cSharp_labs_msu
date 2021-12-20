// pch.h: это предварительно скомпилированный заголовочный файл.
// Перечисленные ниже файлы компилируются только один раз, что ускоряет последующие сборки.
// Это также влияет на работу IntelliSense, включая многие функции просмотра и завершения кода.
// Однако изменение любого из приведенных здесь файлов между операциями сборки приведет к повторной компиляции всех(!) этих файлов.
// Не добавляйте сюда файлы, которые планируете часто изменять, так как в этом случае выигрыша в производительности не будет.

#ifndef PCH_H
#define PCH_H

// Добавьте сюда заголовочные файлы для предварительной компиляции
#include "framework.h"
extern "C"  _declspec(dllexport)
void VM_log_vmdLn(int n, double* arr, double* results_mkl, int& ret, double& time_relations, char* mode_inp);

extern "C"  _declspec(dllexport)
void VM_log_vmsLn(int n, double* arr, double* results_mkl, int& ret, double& time_relations, char* mode_inp);
#endif //PCH_H
