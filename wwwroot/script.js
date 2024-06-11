const sortForm = document.getElementById('sort-form');

const arraySelect = document.getElementById('array-select');
const getArrayButton = document.getElementById('get-array-button');
const arrayResultDiv = document.getElementById('array-result');
// Получаем список ID массивов из БД
fetch('/getNumbers')
    .then(response => response.json())
    .then(data => {
        data.forEach(id => {
            const option = document.createElement('option');
            option.value = id;
            option.text = `Массив ${id}`;
            arraySelect.appendChild(option);
        });
    })
    .catch(error => console.error(error));

getArrayButton.addEventListener('click', async () => {
    try {
        const arrayId = arraySelect.value;
        if (!arrayId) {
            alert('Выберите ID массива');
            return;
        }
        const response = await fetch(`/array?id=${arrayId}`);
        const data = await response.json();
        arrayResultDiv.innerText = `Массив ${arrayId}: ${data.join(", ")}`;
    } catch (error) {
        console.error(error);
        arrayResultDiv.innerText = 'Ошибка при получении массива';
    }
});

sortForm.addEventListener('submit', async (e) => {
    e.preventDefault();
    const arrayInput = document.getElementById('array-input');
    const array = arrayInput.value.split(',').map(Number);
    try {
        const response = await fetch('/sort', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(array)
        });
        const data = await response.json();
        document.getElementById('result').innerHTML = `Отсортированный массив: ${data.join(", ")}`;
    } catch (error) {
        console.error(error);
    }
});