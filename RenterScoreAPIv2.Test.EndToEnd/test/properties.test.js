import axios from 'axios';
import { expect } from 'chai';

describe('properties API testings', () => {
  it('get properties', async () => {
    const response = await axios.get('http://localhost:5000/api/v2/property/properties');
    expect(response.status).to.equal(200);
  });
});