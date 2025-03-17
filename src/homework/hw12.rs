use rand::Rng;

// Функція перевіряє, чи можна розподілити вантаж порівну
fn can_balance(shipments: &Vec<u32>) -> bool {
    let total: u32 = shipments.iter().sum();
    total % (shipments.len() as u32) == 0
}

// Функція рахує мінімальну кількість переміщень вантажу
fn count_permutation(shipments: &Vec<u32>) -> Option<usize> {
    if !can_balance(shipments) {
        return None;
    }
    
    let target = shipments.iter().sum::<u32>() / shipments.len() as u32;
    let mut moves = 0;
    let mut balance = 0;
    
    for &ship in shipments {
        balance += ship as i32 - target as i32;
        moves += balance.abs() as usize;
    }
    
    Some(moves)
}

// Генерує вектор, який можна рівномірно розподілити між кораблями
fn gen_shipments(n: usize) -> Vec<u32> {
    let mut rng = rand::thread_rng();
    let avg_load = rng.gen_range(10..100);
    (0..n).map(|_| avg_load + rng.gen_range(0..5)).collect()
}

fn main() {
    let shipments = gen_shipments(5);
    println!("Згенеровані вантажі: {:?}", shipments);
    
    match count_permutation(&shipments) {
        Some(moves) => println!("Мінімальна кількість переміщень: {}", moves),
        None => println!("Неможливо рівномірно розподілити вантаж."),
    }
}
