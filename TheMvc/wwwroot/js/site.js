// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var ctx = document.getElementById('myChart').getContext('2d');
var male = document.getElementById('male').innerText;
var female = document.getElementById('female').innerText;
var myChart = new Chart(ctx, {
    type: 'bar',
    data: {
        labels: ['Male', 'Female'],
        datasets: [{
            label: '# of Roles',
            data: [male, female],
            backgroundColor: [
                'rgba(255, 99, 132, 0.2)',
                'rgba(54, 162, 235, 0.2)'
            ],
            borderColor: [
                'rgba(255, 99, 132, 1)',
                'rgba(54, 162, 235, 1)'
            ],
            borderWidth: 1
        }]
    },
    options: {
        scales: {
            yAxes: [{
                ticks: {
                    beginAtZero: true
                }
            }]
        }
    }
});
