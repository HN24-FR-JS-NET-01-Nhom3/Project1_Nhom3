// Get today's date
let today = new Date();

// Set countDownDate to today's date by default
let countDownDate = new Date(today.getFullYear(), today.getMonth(), today.getDate(), 18, 30, 0).getTime();

// If it's after 18:30, set countDownDate to 18:30 of the next day
if (today.getHours() > 18 || (today.getHours() === 18 && today.getMinutes() >= 30)) {
    countDownDate = new Date(today.getFullYear(), today.getMonth(), today.getDate() + 1, 18, 30, 0).getTime();
}

// Update the count down every 1 second
let x = setInterval(function() {

    // Get today's date and time
    let now = new Date().getTime();

    // Find the distance between now and the count down date
    let distance = countDownDate - now;

    // Time calculations for days, hours, minutes and seconds
    let hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
    let minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
    let seconds = Math.floor((distance % (1000 * 60)) / 1000);

    // Display the result in the element with id="demo"
    document.getElementById("demo").innerHTML = `
        <table class="table text-center">
            <tr>
                <td>${hours}</td>
                <td>${minutes}</td>
                <td>${seconds}</td>
            </tr>
            <tr class="fw-normal">
                <td>Giờ</td>
                <td>Phút</td>
                <td>Giây</td>
            </tr>
        </table>
    `;

    // If the count down is finished, write some text
    if (distance < 0) {
        clearInterval(x);
        document.getElementById("demo").innerHTML = "EXPIRED";
    }
}, 1000);