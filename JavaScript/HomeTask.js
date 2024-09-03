let sentence = "I can eat bananas all day";
let word = sentence.slice(10, 17);
let uppercasedWord = word.toUpperCase();
alert(uppercasedWord);


let cars = ["Saab", "Volvo", "BMW"];

let bmwValue = cars[2];
console.log(bmwValue);

cars[0] = "Mercedes";
console.log(cars);

cars.pop();
console.log(cars);

cars.push("Audi");
console.log(cars);

cars.splice(1, 1);
console.log(cars);