#include <stdio.h>

#define MAXL 1000

void swap(int *a, int *b)
{
	int t = *a;
	*a = *b;
	*b = t;
}
int main()
{
	int a[MAXL], n, i, sh = 0, b = 0;
	scanf_s("%i", &n);
	for (i = 0; i < n; ++i)
		scanf_s("%i", &a[i]);
	while (1)
	{
		b = 0;
		for (i = 0; i < n; ++i)
		{
			if (i * 2 + 2 + sh < n)
			{
				if (a[i + sh] > a[i * 2 + 1 + sh] || a[i + sh] > a[i * 2 + 2 + sh])
				{
					if (a[i * 2 + 1 + sh] < a[i * 2 + 2 + sh])
					{
						swap(&a[i + sh], &a[i * 2 + 1 + sh]);
						b = 1;
					}
					else if (a[i * 2 + 2 + sh] < a[i * 2 + 1 + sh])
					{
						swap(&a[i + sh], &a[i * 2 + 2 + sh]);
						b = 1;
					}
				}
			}
			else if (i * 2 + 1 + sh < n)
			{
				if (a[i + sh] > a[i * 2 + 1 + sh])
				{
					swap(&a[i + sh], &a[i * 2 + 1 + sh]);
					b = 1;
				}
			}
		}
		if (!b) sh++;
		if (sh + 2 == n)
			break;
	}
	for (i = 0; i < n; ++i)
		printf("%i%c", a[i], (i != n - 1) ? ' ' : '\n');
	return 0;
}