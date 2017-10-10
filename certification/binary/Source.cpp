#include <iostream>

void show_bit(int x)
{
	int mask = 1;

	for (int i = 0; i < sizeof(int) * 8; ++i)
	{
		int res = (x&mask)?1:0;
		std::cout << res;
		mask = mask << 1;
	}

	std::cout << std::endl;
}

int main()
{
	char s[100000];

	std::cin >> s;

	int sixe = strlen(s);

	int k = 7;
	int k2 = k;
	show_bit(k);
	k = k << 1;
	show_bit(k);
	k = k & k2;
	show_bit(k);
	std::cout << k;
	std::cin >> k;

}
