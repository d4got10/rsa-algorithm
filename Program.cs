using RSA;

var rsa = new Rsa();

int p, q;
Console.WriteLine("Выберите вариант задания p, q: (1 - в ручную, 2 - автоматически)");
switch (Console.ReadLine()![0])
{
    case '1':
        Console.WriteLine("Введите p: ");
        p = int.Parse(Console.ReadLine());
        Console.WriteLine("Введите q: ");
        q = int.Parse(Console.ReadLine());
        break;
    case '2':
        var primes = rsa.GeneratePrimes();
        p = primes[0];
        q = primes[1];
        break;
    default:
        throw new Exception("Введены некорректные данные");
}

int n = p * q;
int m = (p - 1) * (q - 1);
int e = rsa.GenerateEncryptor(m);
int d = rsa.GenerateDecryptor(e, m);

bool exit = false;
do
{
    Console.WriteLine("Введите строку: ");
    string input = Console.ReadLine()!;
    Console.WriteLine("Что вы хотите сделать? (1 - зашифровать, 2 - расшифровать, q - выход): ");
    switch (Console.ReadLine()![0])
    {
        case '1':
            var encrypted = rsa.Encrypt(input, e, n);
            Console.WriteLine($"Зашифрованное сообщение: {encrypted}");
            break;
        case '2':
            var decrypted = rsa.Decrypt(input, d, n);
            Console.WriteLine($"Расшифрованное сообщение: {decrypted}");
            break;
        case 'q':
            exit = true;
            break;
    }
} while (!exit);
