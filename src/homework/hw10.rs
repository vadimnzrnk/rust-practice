fn is_palindrome(n: i32) -> bool {
    if n < 0 {
        return false; // Від'ємні числа не можуть бути паліндромами
    }

    let original = n.to_string(); // Перетворюємо число в рядок
    let reversed: String = original.chars().rev().collect(); // Реверсуємо рядок

    original == reversed
}

fn main() {
    let num = 12321;
    if is_palindrome(num) {
        println!("{} є паліндромом.", num);
    } else {
        println!("{} не є паліндромом.", num);
    }
}
