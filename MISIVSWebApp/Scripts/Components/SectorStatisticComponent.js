function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

const Segmento = {
    data() {
        return {
            segmentosLocutor: [],
            selected: 0
        }
    },
    methods: {

    },
    mounted() {

    },
    template:/*html*/`
    <h3>Sector Statistics</h3>
    <hr />
    <div class="row seachBox reloadable_content">
        <div class="col-md-4">
            <div class="abc">
                <p class="center">Choose a Feature to Consult:</p>
            </div>
        </div>
        <select class="col-md-2 selector" id="selector">
            <option selected disabled>Select the variable</option>
            <option v-for="seg in $parent.segmentos" :value="seg.id">{{seg.nombre}}</option>
        </select>
        <div class="col-md-4">
            <div class="abc">
                <button id="btn_search2" class="button" type="button">Search</button>
            </div>
        </div>
    </div>
    `
}

var contenedorSegmentos = new Vue({
    el: '#componente_segmento',
    components: {
        'segmento': Segmento
    },
    data: {
        variables: [],
        segmentos: [],
        selected: 0
    },
    mounted: function () {
        var app = this;
        fetch('/api/emisoras', {
            method: "get",
            credentials: "same-origin",
            headers: {
                "X-CSRFToken": getCookie("csrftoken"),
                "Accept": "application/json",
                "Content-Type": "application/json"
            }
        })
            .then(function (response) {
                return response.json();
            })
            .then(function (myJson) {
                for (var index in myJson) {
                    app.emisoras.push(myJson[index]);
                }
            })
    }
})