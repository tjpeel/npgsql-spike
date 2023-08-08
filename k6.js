import http from 'k6/http';
import { sleep } from 'k6';
import { randomIntBetween } from 'https://jslib.k6.io/k6-utils/1.2.0/index.js';

export const options = {
    vus: 97,
    duration: '30s',
};

export default function () {
    http.get('http://localhost:5064/?name=' + randomIntBetween(1, 100000));
    sleep(1);
}