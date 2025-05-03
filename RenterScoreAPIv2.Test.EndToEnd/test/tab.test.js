import axios from 'axios';
import { expect } from 'chai';

describe('Tab API testing', function () {
  this.timeout(5000);

  it('get home tab', async () => {
    const response = await axios.get('http://localhost:5000/api/v2/tab/home');
    expect(response.status).to.equal(200);

    const tab = response.data;
    validateTab(tab);
    validateHomeTab(tab);
  });

  it('get invalid tab returns 404', async () => {
    try {
      await axios.get('http://localhost:5000/api/v2/tab/invalid');
      throw new Error('Expected error not thrown');
    } catch (error) {
      expect(error.response.status).to.equal(404);
    }
  });
});

function validateTab(tab) {
  expect(tab).to.have.property('cards').that.is.an('array');
  expect(tab.cards).to.not.be.empty;
}

function validateHomeTab(tab) {
  const bannerCard = tab.cards.find(card => card.name === 'banner');
  expect(bannerCard).to.exist;
  expect(bannerCard).to.have.property('type').that.equals('card');
  expect(bannerCard).to.have.property('resources').that.is.an('array');
  
  if (bannerCard.resources.length > 0) {
    const property = bannerCard.resources[0];
    validateProperty(property);
  }

  const categoryCard = tab.cards.find(card => card.name === 'category');
  expect(categoryCard).to.exist;
  expect(categoryCard).to.have.property('type').that.equals('card');
  expect(categoryCard).to.have.property('resources').that.is.an('array');
  
  if (categoryCard.resources.length > 0) {
    const pagingCard = categoryCard.resources[0];
    expect(pagingCard).to.have.property('type').that.equals('card');
    expect(pagingCard).to.have.property('name').that.is.a('string');
    expect(pagingCard).to.have.property('resources').that.is.an('array');
  }
}

function validateProperty(property) {
  expect(property).to.have.property('property').that.is.an('object');
  expect(property.property).to.have.property('property_id').that.is.a('number');
  expect(property.property).to.have.property('property_type').that.is.a('string');
  
  expect(property).to.have.property('user').that.is.an('object');
  expect(property.user).to.have.property('user_id').that.is.a('number');
  
  expect(property).to.have.property('user_profile').that.is.an('object');
  expect(property.user_profile).to.have.property('profile_id').that.is.a('number');
  
  expect(property).to.have.property('property_images').that.is.an('array');
  
  expect(property).to.have.property('property_rating').that.is.an('object');
  expect(property.property_rating).to.have.property('property_id').that.is.a('number');
  expect(property.property_rating).to.have.property('cleanliness').that.is.a('number');
  expect(property.property_rating).to.have.property('traffic').that.is.a('number');
  expect(property.property_rating).to.have.property('amenities').that.is.a('number');
  expect(property.property_rating).to.have.property('safety').that.is.a('number');
  expect(property.property_rating).to.have.property('value_for_money').that.is.a('number');
  expect(property.property_rating).to.have.property('total').that.is.a('number');
} 