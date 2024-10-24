function sayHello() {
    return "Hello";
}

function sayHelloTo(name) {
    return `Hello ${name}!`;
}

function sayHelloManyTimes(name, times) {
    let result = '';
    for (let i = 0; i < times; i++) {
        result += `Hello ${name}! `;
    }
    return result.trim();
}


// 1. Set this variable to 'Hello' by calling the sayHello function
const hello = sayHello('')

// 2. Set this variable variable to 'Hello Jane' calling the sayHelloTo function
const helloToJane = sayHelloTo('Jane')

// 3. Set this variable to 'Hello Bob! Hello Bob! Hello Bob!' calling the sayHelloManyTimes function
const helloToBob3Times = sayHelloManyTimes('Bob', 3)

module.exports = {
    a: hello,
    b: helloToJane,
    c: helloToBob3Times
  }