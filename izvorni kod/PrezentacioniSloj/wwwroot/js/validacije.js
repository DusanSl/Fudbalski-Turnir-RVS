const regsImeStrelca = /^[A-Za-zÀ-žА-яа-яЂ-џ\s]{2,100}$/;
const regsBrojevi = /^[0-9]+$/;

function validirajFormu() {
    var greske = [];

    var terenNaziv = document.querySelector('[name="TerenNaziv"]');
    var terenMesto = document.querySelector('[name="TerenMesto"]');
    var terenAdresa = document.querySelector('[name="TerenAdresa"]');
    var domacinID = document.querySelector('[name="DomacinID"]');
    var gostID = document.querySelector('[name="GostID"]');

    if (terenNaziv && terenNaziv.value.trim() === '')
        greske.push('Naziv terena je obavezan.');

    if (terenMesto && terenMesto.value.trim() === '')
        greske.push('Mesto terena je obavezan.');

    if (terenAdresa && terenAdresa.value.trim() === '')
        greske.push('Adresa terena je obavezna.');

    if (domacinID && domacinID.value === '')
        greske.push('Domaćin je obavezan.');

    if (gostID && gostID.value === '')
        greske.push('Gost je obavezan.');

    if (domacinID && gostID && domacinID.value !== '' && gostID.value !== ''
        && domacinID.value === gostID.value)
        greske.push('Domaćin i gost ne mogu biti isti klub.');

    var redovi = document.querySelectorAll('#telo-tabele tr');
    var prethodniMinut = 0;

    redovi.forEach(function (red, i) {
        var minut = red.querySelector('[name*="MinutGola"]');
        var imeStrelca = red.querySelector('[name*="ImeStrelca"]');
        var klubID = red.querySelector('[name*="KlubID"]');

        if (imeStrelca && !regsImeStrelca.test(imeStrelca.value.trim()))
            greske.push(`Stavka ${i + 1}: Ime strelca nije ispravno (samo slova, min 2 karaktera).`);

        if (minut && !regsBrojevi.test(minut.value))
            greske.push(`Stavka ${i + 1}: Minut mora biti broj.`);

        if (minut && minut.value !== '') {
            var trenutniMinut = parseInt(minut.value);
            if (trenutniMinut < 1 || trenutniMinut > 90)
                greske.push(`Stavka ${i + 1}: Minut mora biti između 1 i 90.`);
            if (trenutniMinut <= prethodniMinut)
                greske.push(`Stavka ${i + 1}: Minut (${trenutniMinut}) mora biti veći od prethodnog (${prethodniMinut}).`);
            prethodniMinut = trenutniMinut;
        }

        if (klubID && klubID.value === '')
            greske.push(`Stavka ${i + 1}: Klub je obavezan.`);
    });

    var poruka = document.getElementById('poruka-validacije');
    if (greske.length > 0) {
        poruka.innerHTML = greske.map(g => `<div>${g}</div>`).join('');
        return false;
    }

    poruka.innerHTML = '';
    return true;
}

document.addEventListener('DOMContentLoaded', function () {
    var forma = document.getElementById('forma-zapisnik');
    if (forma) {
        forma.addEventListener('submit', function (e) {
            if (!validirajFormu()) {
                e.preventDefault();
            }
        });
    }
});