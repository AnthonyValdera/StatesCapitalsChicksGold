var app = angular.module("myApp", []);


app.controller("myCtrl", function ($scope, $http) {
    $scope.questions;
    $scope.quantity = 10;
    $scope.q = 10;

    
    $scope.getQuestions = function () {

        $scope.q = document.getElementById("quantity").value;
        if ($scope.q > 0) {
            $scope.quantity = $scope.q;
        } else {
            if ($scope.q < 1) {
                alert("The number of questions must be greater than 0.");
                return;
            }
        }
        var req = {
            method: 'POST',
            url: 'https://localhost:44374/Dashboard/getQuestions',
            headers: {},
            data: { q: $scope.quantity }
        }
        $http(req).then(result => {
            $scope.questions = result.data;
        });
    }

    
    $scope.SendAnswers = function () {
        let request = [];
        let questions = document.getElementById("questions").getElementsByTagName('li');

        for (let i = 0; i < questions.length; i++) {
            request.push({
                state: questions[i].getElementsByTagName('input')[0].id,
                capital: questions[i].getElementsByTagName('input')[0].value
            })
        }

        for (let i = 0; i < questions.length; i++) {
            capital: questions[i].getElementsByTagName('input')[0].value = "";
        }

        var req = {
            method: 'POST',
            url: 'https://localhost:44374/Dashboard/SendAnswers',
            headers: {},
            data: { request: request, TotalQuestions: questions.length }
        }

        $http(req).then(response => {
            if (response.data.result) {
                alert("Results stored, your rating is: " + response.data.data + "/" + questions.length + ".");
                $scope.getQuestions();
            };
        });


    }
});
