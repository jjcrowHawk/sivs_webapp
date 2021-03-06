﻿const modalInfo = {
    props: {
        inspeccion_id: String,
        id: Number,
        address: String,
        score: String,
        score_weight: String,
    },
    watch: {
        score_data: {
            inmediate: true,
            handler: function (newVal, oldVal) {
                console.log("New Score: " + newVal);
            }
        },
        score_weight_data: function (newVal, oldVal) {
            console.log("New Weight: " + newVal);
        },
        id_data: function (newVal, oldVal) {
            /*this.getVulnerabilityIndex();*/
        }
    },
    data() {
        return {
            inspeccion_id_data: "",
            id_data: 0,
            address_data: "",
            score_data: "",
            score_weight_data: "",
        }
    },
    methods: {
        redirectToPage() {
            console.log("closing_modal");
            //this.$parent.$data["showModal"] = false;
            //$('#componente_modal_info').remove();
            $('#modal-template').remove();
            /*if (this.url_redirect != "") {
                location.href = this.url_redirect;
            }
            else {
                this.$parent.showModal = false;
            }*/
        },
        getVulnerabilityIndex() {
            var instance = this;
            $.get("/Home/GetViviendaVulnerabilityScore/" + this.id_data, function (data, status) {
                //alert("Got RESPONSE!!! " + JSON.stringify(data) + "with status: " + status);
                instance.setScores(data.puntaje, data.puntaje_porcentaje);
                //this.score_data = data.puntaje;
                //this.score_weight_data = data.puntaje_porcentaje;
                //console.log(this.score_weight_data);
            })
        },
        setScores(puntaje, puntaje_porcentaje) {
            this.score_data = puntaje;
            this.score_weight_data = "% " + puntaje_porcentaje;
        },
        getData() {
            this.score_data = this.score;
            this.score_weight_data = this.score_weight;
            this.address_data = this.address;
            this.id_data = this.id;
            this.inspeccion_id_data = this.inspeccion_id;
        }

    },
    created() {

    },
    mounted() {
        this.getData();
        this.getVulnerabilityIndex()
    },
    template: `
    <!-- template for the modal component -->
    <div id="modal-template">
        <transition name="modal">
            <div class="modal-mask">
            <div class="modal-wrapper">
                <div class="modal-container">
                <div class="modal-header" style="text-align:center;background-color:#307FE2">
                   <h3 slot="header" style="margin:auto; color:white; font-weight: bolder;">
                     Vulnerability Report
                    </h3>
                </div>
                <div class="modal-body">
                   <h4>Vulnerability Analysis</h4>
                    <hr />
                    <table>
                        <tr>
                            <td class="td_label">
                                ID:
                            </td>
                            <td class="">
                                {{this.inspeccion_id_data}}
                            </td>
                        </tr>
                        <tr>
                            <td class="td_label">
                                Address:
                            </td>
                            <td>
                                {{this.address_data}}
                            </td>
                        </tr>
                        <tr>
                            <td class="td_label" style="text-decoration: underline;">
                                Vulnerability score:
                            </td>
                            <td>
                                {{this.score_data}}
                            </td>
                            <td class="td_label" style="text-decoration: underline;">
                                Vulnerability percentage:
                            </td>
                            <td>
                                {{this.score_weight_data}}
                            </td>
                        </tr>
                    </table> 
                </div>
                <div class="modal-footer">
                    <button class="modal-default-button btn btn-primary btn-sm" @click="redirectToPage">
                        Close
                    </button>
                </div>
                </div>
            </div>
            </div>
        </transition>
    </div>
    `
}

/* Variable contenedora de la instancia del componente telefono*/
var contenedorModalInfo = new Vue({
    el: '#componente_modal_info',
    data: {
        showModal: false,
        fichasData: []
    },
    components: {
        'modal-info': modalInfo
    },
    methods: {
        showVulnerabilityInfo(index) {
            console.log("on parent: " + index);
        }
    }
})