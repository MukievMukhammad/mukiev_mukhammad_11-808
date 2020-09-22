#include "omp.h"
#include <iostream>
#include <algorithm>

#pragma clang diagnostic push
#pragma ide diagnostic ignored "openmp-use-default-none"

void first() {
#pragma omp parallel num_threads(8)
    {
        int i, n;
        i = omp_get_thread_num();
        n = omp_get_num_threads();
        printf("I’m thread %d of %d, hello world!\n", i, n);
    }
}

void second() {
    omp_set_num_threads(3);
    int x = 3;
#pragma omp parallel if (x > 2)
    {
        int i, n;
        i = omp_get_thread_num();
        n = omp_get_num_threads();
        printf("Thread %d of %d\n", i, n);
    }

    printf("\n");

    omp_set_num_threads(2);
    x = 2;
#pragma omp parallel if(x > 2)
    {
        int i, n;
        i = omp_get_thread_num();
        n = omp_get_num_threads();
        printf("Thread %d of %d\n", i, n);
    }
}

void third() {
    int a = 10, b = 20;

    printf("value of 'a' before -- %d\n", a);
    printf("value of 'b' before -- %d\n\n", b);
    omp_set_num_threads(2);
#pragma omp parallel private(a) firstprivate(b)
    {
        a += omp_get_thread_num();
        b += omp_get_thread_num();
        printf("Tread %d of %d 'a' inside -- %d\n", omp_get_thread_num(), omp_get_num_threads(), a);
        printf("Tread %d of %d 'b' inside -- %d\n", omp_get_thread_num(), omp_get_num_threads(), b);
    }
    printf("\nvalue of 'a' after -- %d\n", a);
    printf("value of 'b' after -- %d\n\n\n\n", b);

    a = 1;
    b = 2;
    printf("value of 'a' before -- %d\n", a);
    printf("value of 'b' before -- %d\n\n", b);
    omp_set_num_threads(4);
#pragma omp parallel private(b)
    {
        a -= omp_get_thread_num();
        b -= omp_get_thread_num();
        printf("Tread %d of %d 'a' inside -- %d\n", omp_get_thread_num(), omp_get_num_threads(), a);
        printf("Tread %d of %d 'b' inside -- %d\n", omp_get_thread_num(), omp_get_num_threads(), b);
    }
    printf("\nvalue of 'a' after -- %d\n", a);
    printf("value of 'b' after -- %d\n\n\n\n", b);
}

void forth() {
    int a[10] = {1, 3, 5, 7, 9, 11, 13, 15, 17, 19};
    int b[10] = {2, 4, 6, 8, 10, 12, 14, 16, 18, 20};
    int min = a[0];
    int max = b[0];
#pragma omp parallel num_threads(2) firstprivate(a)
    {
#pragma omp master
        {
            for (int i; i < 10; i++)
                if (a[i] < min)
                    min = a[i];
        }

#pragma omp single
        {
            for (int i; i < 10; i++)
                if (b[i] > max)
                    max = b[i];
        }

        printf("max -- %d; min -- %d\n", max, min);
//#pragma omp master
//        printf("Thread(%d) min -- %d\n", omp_get_thread_num(), min);
//#pragma omp single
//        printf("Thread(%d) max -- %d\n", omp_get_thread_num(), max);
    }
}

void fifth() {
    int d[6][8];
    for (int i = 0; i < 6; i++)
        for (int j = 0; j < 8; j++)
            d[i][j] = rand() % 100;
#pragma omp parallel sections
    {
#pragma omp section
        {
            int sum = 0;
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 8; j++)
                    sum += d[i][j];
            printf("Thread(%d) avrg = %d\n", omp_get_thread_num(), sum / 48);
        }

#pragma omp section
        {
            int min = d[0][0], max = d[0][0];
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 8; j++) {
                    if (d[i][j] > max) max = d[i][j];
                    if (d[i][j] < min) min = d[i][j];
                }
            printf("Thread(%d) min = %d; max = %d\n", omp_get_thread_num(), min, max);
        }

#pragma omp section
        {
            int count = 0;
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 8; j++)
                    if (d[i][j] % 3 == 0) count++;
            printf("Thread(%d) num%3 = %d", omp_get_thread_num(), count);
        }
    }
}

void sixth() {
    int a[100];
    for (int i = 0; i < 100; i++)
        a[i] = 2;

    int avrg = a[0];
#pragma omp parallel for
    for (int i = 1; i < 100; i++) {
        avrg += a[i];
    }
    printf("arvg parallel for -- %d\n", avrg);

    avrg = a[0];
#pragma omp parallel for reduction(+:avrg)
    for (int i = 1; i < 100; i++) {
        avrg += a[i];
    }
    printf("arvg parallel for reduction -- %d\n", avrg);
}

void seventh() {
    int a[12], b[12], c[12];
#pragma omp parallel num_threads(3)
    {
#pragma omp for schedule(static, 2)
        for (int i = 0; i < 12; i++) {
            a[i] = i;
            b[i] = i;
            printf("Thread[%d/%d] a[%d]=%d; b[%d]=%d\n",
                   omp_get_thread_num(),
                   omp_get_num_threads(),
                   i,
                   a[i],
                   i,
                   b[i]);
        }
    }

#pragma omp parallel num_threads(4)
    {
#pragma omp for schedule(dynamic, 3)
        for (int i = 0; i < 12; i++) {
            c[i] = a[i] + b[i];
            printf("Thread[%d/%d] c[%d]=%d\n",
                   omp_get_thread_num(),
                   omp_get_num_threads(),
                   i,
                   c[i]);
        }
    }
}

void eighth() {
    int a[16000];
    for (int i = 0; i < 16000; i++)
        a[i] = i;
    int min_time, min_c;

    for (int j = 1; j < 16000; j++) {
        unsigned int start_time1 = clock();
        int b1[15998];
#pragma omp parallel for schedule(guided, j) num_threads(8)
        for (int i = 1; i < 15999; i++)
            b1[i] = (a[i - 1] + a[i] + a[i + 1]) / 3;
        unsigned int end_time1 = clock();
        printf("time = %d\nc=%d\n", end_time1 - start_time1, j);
        if (j == 1) min_time = end_time1 - start_time1;
        if ((end_time1 - start_time1) < min_time) {
            min_time = end_time1 - start_time1;
            min_c = j;
        }
    }

    printf("Optimal \ntime = %d;\nc=%d;\n", min_time, min_c);
}

void ninth() {
    int x = 100, y = 100;
    int non_parallel_time = 0, parallel_time = 1;

    while (parallel_time > non_parallel_time) {
        x++, y++;
        int matrix[y][x], vector[x];

        unsigned int start_time_non_parallel = clock();
        int result_non_p[x];
        for (int i = 0; i < y; i++)
            for (int j = 0; j < x; j++)
                result_non_p[y] += matrix[i][j] * vector[j];
        unsigned int end_time_non_parallel = clock();
        non_parallel_time = end_time_non_parallel - start_time_non_parallel;
        printf("Non parallel time -- %d\n", non_parallel_time);

        int r_runtime[x];
        unsigned int start_time_runtime = clock();
#pragma omp parallel
#pragma omp for schedule(static)
        for (int i = 0; i < y; i++)
            for (int j = 0; j < x; j++)
                r_runtime[y] += matrix[i][j] * vector[j];
        unsigned int end_time_runtime = clock();
        parallel_time = end_time_runtime - start_time_runtime;
        printf("runtime parallel time -- %d\n", parallel_time);
    }

    printf("x = %d; y = %d;", x, y);
}

void tenth() {
    int d[6][8];
    for (int i = 0; i < 6; i++)
        for (int j = 0; j < 8; j++)
            d[i][j] = rand() % 100;
    int max = d[0][0], min = d[0][0];
    int g_max = d[0][0], g_min = d[0][0];
#pragma omp parallel num_threads(6) firstprivate(max, min)
    {
#pragma omp for
        for (int i = 0; i < 6; i++)
            for (int j = 0; j < 8; j++) {
                if (d[i][j] > max)
                    max = d[i][j];
                if (d[i][j] < min)
                    min = d[i][j];
            }
#pragma omp critical
        {
            if (max > g_max)
                g_max = max;
            if (min < g_min)
                g_min = min;
        }
    }
    printf("min = %d;\nmax = %d;\n", g_min, g_max);
}

void eleventh() {
    int a[30], count = 0;
    for (int i = 0; i < 30; i++)
        a[i] = rand();
#pragma omp parallel num_threads(4)
    for (int i = 0; i < 30; i++)
        if (a[i] % 9 == 0)
#pragma omp atomic
            count++;
    printf("result: %d", count);
}

void twelfth() {
    int a[1000];
    for (int i = 0; i < 1000; i++)
        a[i] = rand();
    int max = a[0];
#pragma omp parallel for num_threads(4)
    for (int i = 0; i < 1000; i++)
        if (a[i] % 7)
#pragma omp critical
            if (a[i] > max) max = a[i];
    printf("result = %d", max);
}

void thirteenth() {
#pragma omp parallel num_threads(8)
    {
        int n = omp_get_num_threads();
        for (int i = n - 1; i >= 0; i--) {
#pragma omp barrier
            if (i == omp_get_thread_num())
#pragma omp critical
                printf("Thread(%d/%d)\n", i, n);

        }
    }
}

void thirteenth2() {
#pragma omp parallel for ordered
    for (int i = 0; i < omp_get_num_threads(); i++) {
        int n = omp_get_num_threads() - i;
#pragma omp ordered
        printf("%d) Thread(%d/%d)\n",
               n,
               omp_get_thread_num(),
               omp_get_num_threads());
    }
}

int main() {
    tenth();
    return 0;
}

#pragma clang diagnostic pop