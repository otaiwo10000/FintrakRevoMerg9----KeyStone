/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("UpLoadCSVFileEditController",
            ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator', '$rootScope', '$http', 'fileUploadService',
                        UpLoadCSVFileEditController]);

    function UpLoadCSVFileEditController($scope, $window, $state, $stateParams, viewModelHelper, validator, $rootScope, $http, fileUploadService) {
        var vm = this;
        vm.acctmisoverridedisplay = false;
        vm.acctlistingdisplay = false;
        vm.acctmisoverridedisplayTEXT = "Expand: ";
        vm.acctlistingdisplayTEXT = "Expand: ";

        vm.acctmisbtndisable = false;
        vm.acctlistingbtndisable = false;
        
        vm.acctmisoverridedisplayFunc = function () {
            vm.acctmisoverridedisplay = !vm.acctmisoverridedisplay;
            acctmisoverridedisplayTEXTFunc();
        };

        vm.acctlistingdisplayFunc = function () {
            vm.acctlistingdisplay = !vm.acctlistingdisplay;
            acctlistingdisplayTEXTFunc();
        };

        var acctmisoverridedisplayTEXTFunc = function () {
            if (vm.acctmisoverridedisplay === true) { vm.acctmisoverridedisplayTEXT = "Collapse: "; }
            else { vm.acctmisoverridedisplayTEXT = "Expand: "; }
        };

        var acctlistingdisplayTEXTFunc = function () {
            if (vm.acctlistingdisplay === true) { vm.acctlistingdisplayTEXT = "Collapse: "; }
            else { vm.acctlistingdisplayTEXT = "Expand: "; }
        };

        vm.acctmisbtndisableFunc = function () {
            vm.acctmisbtndisable = true;
        };

        vm.acctmisonchangedisableFunc = function () {
            vm.acctmisbtndisable = false;
        };

        vm.acctlistingbtndisableFunc = function () {
            vm.acctlistingbtndisable = true;
        };

        vm.acctlistingonchangedisableFunc = function () {
            vm.acctlistingbtndisable = false;
        };

        //////----------------------------- the below starts ----- it has been tested and work fine -----

        //$scope.getFileDetails = function (e) {

        //    $scope.files = [];
        //    $scope.$apply(function () {

        //        // STORE THE FILE OBJECT IN AN ARRAY.
        //        for (var i = 0; i < e.files.length; i++) {
        //            $scope.files.push(e.files[i]);
        //        }

        //    });
        //};

        //// NOW UPLOAD THE FILES.
        //$scope.uploadFiles = function () {

        //    //FILL FormData WITH FILE DETAILS.
        //    var data = new FormData();

        //    for (var i in $scope.files) {
        //        data.append("csvfile", $scope.files[i]);
        //    }

        //    //// ADD LISTENERS.
        //    var objXhr = new XMLHttpRequest();
        //    //objXhr.addEventListener("progress", updateProgress, false);
        //    //objXhr.addEventListener("load", transferComplete, false);

        //    //// SEND FILE DETAILS TO THE API.
        //    objXhr.open("POST", "api/uploadcsvfile/incomeaccountmisoverride");
        //    objXhr.send(data);
        //    vm.res = objXhr.response;
        //    vm.res2 = objXhr.responseText;
        //    return vm.res + ' ' + vm.res2;
        //};

        //////----------------------------- the above ends ---------------------------------------------------

        //////----------------------------- the below starts -------------------------------------------------

        
        //$scope.uploadFile = function () {
        //    var file = $scope.myFile;
        //    var uploadUrl = "api/uploadcsvfile/incomeaccountmisoverride", //Url of webservice/api/server
        //        promise = fileUploadService.uploadFileToUrl(file, uploadUrl);

        //    promise.then(function (response) {
        //        $scope.serverResponse = response;
        //    }, function () {
        //        $scope.serverResponse = 'An error has occurred';
        //    });
        //};

        //// //----------------------------- the above ends ---------------------------------------------------


        //////----------------------------- the below starts -------------------------------------------------

        //$scope.getFileDetails = function (e) {
        //    $scope.files = [];

        //    $scope.$apply(function () {

        //        // STORE THE FILE OBJECT IN AN ARRAY.
        //        for (var i = 0; i < e.files.length; i++) {
        //            $scope.files.push(e.files[i]);
        //        }
        //    });
        //};

        // // NOW UPLOAD THE FILES.
        //$scope.uploadFiles = function () {

        //    //FILL FormData WITH FILE DETAILS.
        //    var formdata = new FormData();

        //    for (var i in $scope.files) {
        //        formdata.append("csvfile", $scope.files[i]);
        //    }

        //    var request = {
        //        method: 'POST',
        //        url: 'api/uploadcsvfile/incomeaccountmisoverride',
        //        data: formdata,
        //        contentType: false,
        //        processData: false,
        //        headers: {
        //            'Content-Type': undefined
        //        }
        //    };

        //    // SEND THE FILES.
        //    $http(request)
        //        .success(function (d) {
        //            alert(d);
        //        })
        //        .error(function () {
        //        });
        //};

        

        //// //----------------------------- the above ends --------------------------------------------------

       
    }
}());
