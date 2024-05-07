let parts = window.location.href.split('/');

let currentDay = new Date().getDate();
let currentMonth = new Date().getMonth();
let currentYear = new Date().getFullYear();

if (parts.length >= 7) {
    currentDay = parseInt(parts[parts.length - 1]);
    currentMonth = parseInt(parts[parts.length - 2]) - 1;
    currentYear = parseInt(parts[parts.length - 3]);
}

let selectYear = document.getElementById("year");
let selectMonth = document.getElementById("month");

selectYear.value = currentYear;
selectMonth.value = currentMonth;
showCalendar(currentDay, currentMonth, currentYear);

function jump() {
    showCalendar(currentDay, parseInt(selectMonth.value), parseInt(selectYear.value));
    selectYear.value = parseInt(selectYear.value);
    selectMonth.value = parseInt(selectMonth.value);
}

function showCalendar(day, month, year) {

    let firstDay = (new Date(year, month)).getDay();
    let daysInMonth = 32 - new Date(year, month, 32).getDate();

    let tbl = document.getElementById("calendar-body");

    // clearing all previous cells
    tbl.innerHTML = "";

    // creating all cells
    let date = 1;
    for (let i = 0; i < 6; i++) {
        // creates a table row
        let row = document.createElement("tr");

        //creating individual cells, filing them up with data.
        for (let j = 0; j < 7; j++) {
            if (i === 0 && j < firstDay) {
                let cell = document.createElement("td");
                let cellText = document.createTextNode("");
                cell.appendChild(cellText);
                row.appendChild(cell);
            } else if (date > daysInMonth) {
                break;
            } else {
                let cell = document.createElement("td");
                let cellText = document.createTextNode(date.toString());
                
                // coloring today's date
                if (date === day && year === currentYear && month === currentMonth) {
                    cell.classList.add("bg-info");
                } 
                cell.classList.add("btn-date");

                let cellLink = document.createElement("a");
                cellLink.href = `/lottery/${year}/${month+1}/${date}`;
                cellLink.appendChild(cellText);
                cell.appendChild(cellLink);
                row.appendChild(cell);
                date++;
            }
        }
        tbl.appendChild(row); // appending each row into calendar body.
    }
}
