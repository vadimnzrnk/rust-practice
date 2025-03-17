use rand::Rng;
use itertools::permutations;

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

// Функція для підрахунку зайнятої площі
fn calculate_occupied_area(dimensions: &[(u32, u32)]) -> u32 {
    dimensions.iter().map(|(w, h)| w * h).sum()
}

// Функція для знаходження змінних m, u, x, a, s, l, o, n
fn solve_muxa_slon() -> Vec<(u32, u32, u32, u32, u32, u32, u32, u32)> {
    let digits: Vec<u32> = (1..=9).collect();
    let mut solutions = Vec::new();
    
    for perm in permutations(digits, 8) {
        let (m, u, x, a, s, l, o, n) = (perm[0], perm[1], perm[2], perm[3], perm[4], perm[5], perm[6], perm[7]);
        let muxa = m * 1000 + u * 100 + x * 10 + a;
        let slon = s * 1000 + l * 100 + o * 10 + n;
        
        if muxa * a == slon {
            solutions.push((m, u, x, a, s, l, o, n));
        }
    }
    
    solutions
}

fn main() {
    let shipments = gen_shipments(5);
    println!("Згенеровані вантажі: {:?}", shipments);
    
    match count_permutation(&shipments) {
        Some(moves) => println!("Мінімальна кількість переміщень: {}", moves),
        None => println!("Неможливо рівномірно розподілити вантаж."),
    }
    
    let areas = vec![(4, 5), (3, 7), (6, 2)];
    println!("Зайнята площа: {}", calculate_occupied_area(&areas));
    
    let solutions = solve_muxa_slon();
    println!("Знайдено {} рішень:", solutions.len());
    for (m, u, x, a, s, l, o, n) in &solutions {
        println!("  {}{}{}{}\n x     {}\n -------\n   {}{}{}{}\n", m, u, x, a, a, s, l, o, n);
    }
}
