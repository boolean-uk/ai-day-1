function increment(number) {
    return number + 1;
}

function capitalize(str) {
    return str.charAt(0).toUpperCase() + str.slice(1);
}

function nameAndSmile(name) {
    return capitalize(name) + " :)";
}

function countStrings(arr) {
    return arr.filter(item => typeof item === 'string').length;
}

function amazing(obj) {
    if (!obj.hasOwnProperty('edward')) {
        obj.edward = 'amazing';
    }
    return obj;
}

module.exports = {
    a: increment, // 1. change undefined to be the name of the function you defined for the first TODO
    b: capitalize, // 2. change undefined to be the name of the function you defined for the second TODO)
    c: nameAndSmile, // etc
    d: countStrings,
    e: amazing
  }