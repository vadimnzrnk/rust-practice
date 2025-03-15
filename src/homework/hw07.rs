fn swap_case(s: &str) -> String {
    s.chars()
        .map(|c| {
            if c.is_lowercase() {
                c.to_ascii_uppercase()
            } else {
                c.to_ascii_lowercase()
            }
        })
        .collect()
}

fn main() {
    let input = "Rust Програмування 123";
    let output = swap_case(input);
    println!("Вхідний рядок: {}", input);
    println!("Результат: {}", output);
}

fn swap_case(s: &str) -> String {
    s.chars()
        .map(|c| {
            if c.is_lowercase() {
                c.to_ascii_uppercase()
            } else if c.is_uppercase() {
                c.to_ascii_lowercase()
            } else {
                c
            }
        })
        .collect()
}

fn main() {
    let input = "Hello, Rust!";
    let swapped = swap_case(input);
    println!("Original: {}", input);
    println!("Swapped: {}", swapped);
}
