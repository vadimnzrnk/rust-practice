fn is_prime(n: u64) -> bool {
    if n < 2 {
        return false;
    }
    if n == 2 || n == 3 {
        return true;
    }
    if n % 2 == 0 || n % 3 == 0 {
        return false;
    }

    let mut i = 5;
    while i * i <= n {
        if n % i == 0 || n % (i + 2) == 0 {
            return false;
        }
        i += 6;
    }
    true
}

fn main() {
    let num = 29; // можна змінити число для перевірки
    if is_prime(num) {
        println!("{} є простим числом.", num);
    } else {
        println!("{} не є простим числом.", num);
    }
}
